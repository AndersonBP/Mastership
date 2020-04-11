using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Mastership.Domain.DTO;
using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application {
    public interface IEmailApplication {
        Task SendEmailAsync(string subject, string body, string email);
    }
}
