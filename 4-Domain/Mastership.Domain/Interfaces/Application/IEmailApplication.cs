using System;
using System.Threading.Tasks;
using Mastership.Domain.DTO;

namespace Mastership.Domain.Interfaces.Application {
    public interface IEmailApplication {
        Task SendEmailAsync(PointTimeDTO pointTimeDTO);
    }
}
