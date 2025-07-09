using AppVinhosFinal.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Resend;

namespace AppVinhosFinal.Services
{
    public class ResendService(IResend resendClient, IOptions<ResendOptions> resendOptions) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new EmailMessage
            {
                From = resendOptions.Value.EmailFrom,
                Subject = subject,
                HtmlBody = htmlMessage
            };
            message.To.Add(email);

            await resendClient.EmailSendAsync(message);
        }
    }
}
