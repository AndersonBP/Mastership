using System;
using System.Threading.Tasks;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Microsoft.Extensions.Options;

namespace Mastership.Application.Services {
    public class EmailApplication : IEmailApplication {
        private readonly EmailSettingsDTO _emailSettingsDTO;

        public EmailApplication(
            IOptions<EmailSettingsDTO> emailSetting
        ) {
            this._emailSettingsDTO = emailSetting.Value;
        }

        public Task SendEmailAsync(PointTimeDTO pointTimeDTO) {
            try {
                // TODO: send e-mail
                return Task.FromResult(0);
            } catch (Exception) {
                throw;
            }
        }
    }
}
