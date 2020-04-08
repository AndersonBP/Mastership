using System;
using System.Threading.Tasks;
using Mastership.Domain.DTO;
using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application {
    public interface IEmailApplication {
        Task SendEmailAsync(PointTimeDTO pointTimeDTO, CheckRegistrationViewModel checkRegistration);
    }
}
