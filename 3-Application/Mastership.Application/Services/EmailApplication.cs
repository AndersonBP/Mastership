using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Mastership.Domain.DTO;
using Mastership.Domain.Enum;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Mastership.Application.Services
{
    public class EmailApplication : IEmailApplication
    {
        private readonly EmailSettingsDTO _emailSettings;
        private readonly ITemplateService _templateService;

        public EmailApplication(
            IOptions<EmailSettingsDTO> emailSettings, ITemplateService templateService
        )
        {
            this._emailSettings = emailSettings.Value;
            this._templateService = templateService;
        }

        public Task SendEmailAsync(string subject, string body, string email )
        {
            try
            {
                var mail = this.NewMail(subject);
                mail.Body = body;
                mail.To.Add(new MailAddress(email));
                this.SendEmail(new List<MailMessage>() { mail });
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task SendEmail(IEnumerable<MailMessage> mails)
        {
            foreach (var mail in mails)
            {
                using (SmtpClient smtp = new SmtpClient(this._emailSettings.Server, this._emailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(this._emailSettings.Email, this._emailSettings.Password);
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(mail);
                }
            }
        }

        public MailMessage NewMail(string subject)
        {
            return new MailMessage()
            {
                From = new MailAddress(this._emailSettings.Email, "RH Gestão"),
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
                Subject = subject
            };
        }
    }
}
