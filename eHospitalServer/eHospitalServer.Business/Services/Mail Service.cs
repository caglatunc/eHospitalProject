using GenericEmailService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Business.Services;
public static class MailService
{
    public static async Task<string> SendEmailAsync(string email, string subject, string body)
    {
        EmailConfigurations configurations = new(
            "smtp.office365.com",
           "Parola1!",
            587,
            false,
            true);

        List<string> emails = new() { email };

        EmailModel<Stream> model = new(
               configurations,
               "iyzico1@outlook.com",
                emails,
                subject,
                 body,
                 null);

        string response = await EmailService.SendEmailWithMailKitAsync(model);

        return response;
    }
}
