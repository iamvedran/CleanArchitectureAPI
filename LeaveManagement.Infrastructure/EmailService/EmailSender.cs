using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LeaveManagement.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings EmailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmail(EmailMessage email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);
            var to = new EmailAddress(email.To);

            var from = new EmailAddress
            {
                Email = EmailSettings.ApiKey,
                Name = EmailSettings.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}
