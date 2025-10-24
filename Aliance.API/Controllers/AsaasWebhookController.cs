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
        private readonly string _logFilePath;
        private readonly IMailSending _mailSending;

        public AsaasWebhookController(ILogger<AsaasWebhookController> logger, IChurchService churchService, IMailSending mailSending)
        {
            _logger = logger;
            _churchService = churchService;

            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            _logFilePath = Path.Combine(logDirectory, "AsaasWebhook.log");
            _mailSending = mailSending;
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


                // Extrai o customerId
                string? customerId = payload.Value.GetProperty("customer").GetString();

                // Busca a igreja vinculada a este cliente Asaas
                var church = await _churchService.GetChurchByAsaasCustomerId(customerId);
                if (church == null)
                {
                    await LogAsync($"Igreja não encontrada para customerId={customerId}");
                    return NotFound($"Igreja não encontrada para customerId={customerId}");
                }

                switch (eventType)
                {
                    case "PAYMENT_CONFIRMED":
                        {
                            var value = payload.Value.GetProperty("value").GetDecimal();
                            var paymentDateStr = payload.Value.TryGetProperty("paymentDate", out var pd) ? pd.GetString() : null;
                            var nextDueDateStr = payload.Value.TryGetProperty("nextDueDate", out var nd) ? nd.GetString() : null;

                            DateTime paymentDate = ParseDate(paymentDateStr) ?? DateTime.UtcNow;
                            DateTime? nextDueDate = ParseDate(nextDueDateStr);

                            await LogAsync($"Pagamento recebido: customer={customerId}, valor={value}, pagoEm={paymentDate:yyyy-MM-dd}, próxima cobrança={nextDueDate:yyyy-MM-dd}");

                            var firstCustomerMail = await _churchService.GetChurchesFirstUser(customerId);
                            await _churchService.AtualizarPagamentoRecebidoAsync(customerId, paymentDate, nextDueDate, value);
                            Console.WriteLine("---------------------------");
                            Console.WriteLine($"ENVIANDO EMAIL PARA: {firstCustomerMail}");
                            Console.WriteLine("---------------------------");
                            await _mailSending.SendEmailAsync(firstCustomerMail, "teste envio de email", "plain text", "html");
                            break;
                        }

                    case "PAYMENT_OVERDUE":
                        {
                            var dueDateStr = payload.Value.TryGetProperty("dueDate", out var dd) ? dd.GetString() : null;
                            DateTime dueDate = ParseDate(dueDateStr) ?? DateTime.UtcNow;

                            await LogAsync($"Pagamento atrasado: customer={customerId}, vencimento={dueDate:yyyy-MM-dd}");
                            await _churchService.AtualizarPagamentoAtrasadoAsync(customerId, dueDate);
                            break;
                        }

                    case "SUBSCRIPTION_CANCELED":
                        {
                            var subscriptionId = payload.Value.GetProperty("id").GetString();
                            await LogAsync($"Assinatura cancelada: customer={customerId}, subscription={subscriptionId}");
                            
                            break;
                        }

                    case "SUBSCRIPTION_CREATED":
                        {
                            var subscriptionId = payload.Value.GetProperty("id").GetString();
                            var nextDueDateStr = payload.Value.TryGetProperty("nextDueDate", out var nd) ? nd.GetString() : null;
                            await LogAsync($"Assinatura criada: customer={customerId}, subscription={subscriptionId}, próxima cobrança={nextDueDateStr}");
                            
                            break;
                        }

                    default:
                        await LogAsync($"Evento não tratado: {eventType}");
                        break;
                }

                return Ok(new { status = "success" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar webhook do Asaas");
                await LogAsync($"Erro: {ex.Message}");
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
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

            // Asaas geralmente envia datas no formato yyyy-MM-dd ou dd/MM/yyyy
            if (DateTime.TryParseExact(dateStr, new[] { "yyyy-MM-dd", "dd/MM/yyyy", "yyyy-MM-dd HH:mm:ss" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return null;
        }
    }
}
