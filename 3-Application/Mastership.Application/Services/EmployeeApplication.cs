using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class EmployeeApplication : BaseApplication<EmployeeViewModel, EmployeeEntity, IEmployeeRepository>, IEmployeeApplication
    {
        public EmployeeApplication(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
