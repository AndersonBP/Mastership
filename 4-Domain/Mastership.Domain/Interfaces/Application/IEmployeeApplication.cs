using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IEmployeeApplication : IApplication<EmployeeViewModel> {
        EmployeeViewModel CheckRegistration(EmployeeViewModel vm, string companyName);
    }
}
