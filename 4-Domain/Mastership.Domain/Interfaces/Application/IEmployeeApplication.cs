using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IEmployeeApplication : IApplication<EmployeeViewModel> {
        CheckRegistrationViewModel CheckRegistration(CheckRegistrationViewModel vm, string companyName);
    }
}
