using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsaasWebhookController : ControllerBase
{
    private readonly ILogger<AsaasWebhookController> _logger;
    private readonly string _logFilePath;

    public AsaasWebhookController(ILogger<AsaasWebhookController> logger)
    {
        _logger = logger;

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
            // Lê o corpo bruto da requisição
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            // Grava o JSON recebido no arquivo de log
            _logger.LogInformation("Webhook recebido: {body}", body);
            await System.IO.File.AppendAllTextAsync(_logFilePath, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {body}{Environment.NewLine}");

            // Retorno simples para o Asaas
            return Ok(new { status = "success" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar webhook do Asaas");
            return StatusCode(500, new { status = "error", message = ex.Message });
        }
    }
}
