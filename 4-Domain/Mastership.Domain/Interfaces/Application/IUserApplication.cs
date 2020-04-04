using Mastership.Domain.DTO;
using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IUserApplication : IApplication<UserViewModel> {
        UserViewModel Authenticate(string domain, LoginViewModel loginViewModel);
    }
}
