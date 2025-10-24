using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Mailing;

public interface IMailSending
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);
}
