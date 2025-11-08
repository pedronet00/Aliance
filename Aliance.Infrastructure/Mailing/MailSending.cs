using Aliance.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aliance.Infrastructure.Mailing;

public class MailSending : IMailService
{
    private readonly IConfiguration _config;

    public MailSending(IConfiguration config)
    {
        _config = config;
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
    {
        var apiKey = _config["SendGrid:ApiKey"];

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("suporte@aliance.app.br", "Aliance | ERP para igrejas");
        
        var to = new EmailAddress(toEmail);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return true;
    }
}
