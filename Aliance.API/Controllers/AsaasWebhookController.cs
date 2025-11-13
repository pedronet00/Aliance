using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Aliance.Application.Interfaces;
using Aliance.Infrastructure.Mailing;

namespace Aliance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsaasWebhookController : ControllerBase
    {
        private readonly ILogger<AsaasWebhookController> _logger;
        private readonly IChurchService _churchService;
        private readonly IMailService _mailSending;
        private readonly IUserService _userService;
        private readonly string _logFilePath;

        public AsaasWebhookController(
            ILogger<AsaasWebhookController> logger,
            IChurchService churchService,
            IMailService mailSending,
            IUserService userService)
        {
            _logger = logger;
            _churchService = churchService;
            _mailSending = mailSending;
            _userService = userService;

            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            _logFilePath = Path.Combine(logDirectory, "AsaasWebhook.log");
        }

        [HttpPost]
        public async Task<IActionResult> Receive()
        {
            try
            {
                using var reader = new StreamReader(Request.Body);
                var body = await reader.ReadToEndAsync();
                await LogAsync($"Webhook recebido: {body}");

                using var jsonDoc = JsonDocument.Parse(body);
                var root = jsonDoc.RootElement;

                if (!root.TryGetProperty("event", out var eventProp))
                    return BadRequest("Campo 'event' ausente.");

                string eventType = eventProp.GetString() ?? string.Empty;
                JsonElement? payload = null;

                if (root.TryGetProperty("payment", out var paymentElement))
                    payload = paymentElement;
                else if (root.TryGetProperty("subscription", out var subscriptionElement))
                    payload = subscriptionElement;
                else
                    await LogAsync($"⚠️ Nenhum payload de 'payment' ou 'subscription' encontrado no evento {eventType}.");

                if (payload is null)
                    return Ok();

                // Extrai o customerId
                string? customerId = payload.Value.TryGetProperty("customer", out var custEl) ? custEl.GetString() : null;
                if (string.IsNullOrEmpty(customerId))
                {
                    await LogAsync("⚠️ Webhook recebido sem 'customerId'. Ignorado.");
                    return Ok();
                }

                // Lógica principal
                switch (eventType)
                {
                    case "PAYMENT_CONFIRMED":
                    case "PAYMENT_RECEIVED":
                        await HandlePaymentConfirmedAsync(payload.Value, customerId);
                        break;

                    case "PAYMENT_OVERDUE":
                        await HandlePaymentOverdueAsync(payload.Value, customerId);
                        break;

                    case "SUBSCRIPTION_CREATED":
                        await LogAsync($"Assinatura criada para customer={customerId}");
                        break;

                    case "SUBSCRIPTION_CANCELED":
                        await LogAsync($"Assinatura cancelada para customer={customerId}");
                        break;

                    case "CHECKOUT_CREATED":
                        await LogAsync($"Checkout criado para customer={customerId}");
                        break;

                    default:
                        await LogAsync($"Evento não tratado: {eventType}");
                        break;
                }

                return Ok(new { status = "success" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar webhook do Asaas");
                await LogAsync($"❌ Erro: {ex.Message}");
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        private async Task HandlePaymentConfirmedAsync(JsonElement payment, string customerId)
        {
            var value = payment.TryGetProperty("value", out var v) ? v.GetDecimal() : 0;
            var paymentDateStr = payment.TryGetProperty("paymentDate", out var pd) ? pd.GetString() : null;
            var nextDueDateStr = payment.TryGetProperty("nextDueDate", out var nd) ? nd.GetString() : null;

            DateTime paymentDate = ParseDate(paymentDateStr) ?? DateTime.UtcNow;
            DateTime? nextDueDate = ParseDate(nextDueDateStr);

            await LogAsync($"💰 Pagamento confirmado - customer={customerId}, valor={value}, data={paymentDate:yyyy-MM-dd}");

            // Atualiza informações financeiras
            await _churchService.AtualizarPagamentoRecebidoAsync(customerId, paymentDate, nextDueDate, value);

            // Envia e-mail de boas-vindas / redefinição de senha
            var firstCustomerMail = await _churchService.GetChurchesFirstUser(customerId);
            var passwordResetUrl = await _userService.GeneratePasswordResetUrl(customerId, null);

            await _mailSending.SendEmailAsync(
                firstCustomerMail,
                "Bem-vindo ao Aliance ERP",
                "plain text",
                $"Seu pagamento foi confirmado com sucesso. Defina sua senha e acesse sua conta: <a href='{passwordResetUrl}'>Definir senha</a>"
            );
        }

        private async Task HandlePaymentOverdueAsync(JsonElement payment, string customerId)
        {
            var dueDateStr = payment.TryGetProperty("dueDate", out var dd) ? dd.GetString() : null;
            DateTime dueDate = ParseDate(dueDateStr) ?? DateTime.UtcNow;

            await LogAsync($"⚠️ Pagamento atrasado: customer={customerId}, vencimento={dueDate:yyyy-MM-dd}");
            await _churchService.AtualizarPagamentoAtrasadoAsync(customerId, dueDate);
        }

        private async Task LogAsync(string message)
        {
            var logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {message}{Environment.NewLine}";
            _logger.LogInformation(message);
            await System.IO.File.AppendAllTextAsync(_logFilePath, logMessage);
        }

        private static DateTime? ParseDate(string? dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return null;

            if (DateTime.TryParseExact(dateStr,
                new[] { "yyyy-MM-dd", "dd/MM/yyyy", "yyyy-MM-dd HH:mm:ss" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return null;
        }
    }
}
