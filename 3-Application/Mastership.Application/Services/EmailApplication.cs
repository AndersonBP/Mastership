﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Mastership.Domain.DTO;
using Mastership.Domain.Enum;
using Mastership.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Mastership.Application.Services {
    public class EmailApplication : IEmailApplication {
        private readonly EmailSettingsDTO _emailSettings;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmailApplication(
            IOptions<EmailSettingsDTO> emailSettings,
            IHostingEnvironment hostingEnvironment
        ) {
            this._emailSettings = emailSettings.Value;
            this._hostingEnvironment = hostingEnvironment;
        }

        public Task SendEmailAsync(PointTimeDTO pointTimeDTO) {
            try {
                var mail = this.WriteEmailToClocking(pointTimeDTO);
                this.SendEmail(new List<MailMessage>() { mail });
                return Task.FromResult(0);
            } catch (Exception) {
                throw;
            }
        }

        private MailMessage WriteEmailToClocking(PointTimeDTO pointTimeDTO) {
            var mail = this.NewMail("Registro de ponto");
            var template = this.GetTemplate(EmailType.Clocking);
            mail.Body = template;
            // TODO: Alterar template
            
            mail.To.Add(new MailAddress(pointTimeDTO.Employee.Email));
            return mail;
        }

        public string GetTemplate(EmailType emailType) {
            var fileName = string.Empty;
            switch(emailType) {
                case EmailType.Clocking:
                    fileName = "clocking_template.html";
                    break;
                default:
                    throw new Exception();
            }

            var directoryPath = this._hostingEnvironment.ContentRootPath + @"/wwwroot/EmailTemplates/";
            return System.IO.File.ReadAllText(directoryPath + fileName);
        }

        public async Task SendEmail(IEnumerable<MailMessage> mails) {
            foreach (var mail in mails) {
                using (SmtpClient smtp = new SmtpClient(this._emailSettings.Server, this._emailSettings.Port)) {
                    smtp.Credentials = new NetworkCredential(this._emailSettings.Email, this._emailSettings.Password);
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(mail);
                }
            }
        }

        public MailMessage NewMail(string subject) {
            return new MailMessage() {
                From = new MailAddress(this._emailSettings.Email, "RH Gestão"),
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
                Subject = subject
            };
        }
    }
}
