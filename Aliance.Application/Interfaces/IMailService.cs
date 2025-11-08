using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IMailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);
}
