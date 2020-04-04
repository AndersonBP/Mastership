using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class SubsidiaryApplication : BaseApplication<SubsidiaryViewModel, SubsidiaryDTO, ISubsidiaryRepository>, ISubsidiaryApplication
    {
        public readonly IEmployeeRepository _employeeRepository;

        public SubsidiaryApplication(
            IMapper mapper,
            ISubsidiaryRepository repository,
            IEmployeeRepository employeeRepository
        ) : base(repository, mapper) {
            this._employeeRepository = employeeRepository;
        }

        public SubsidiaryViewModel CheckDomainName(string domainName)
        {
            var subsidiary = this.MapToViewModel(this.Repository.GetByDomainName(domainName));
            if (subsidiary == null)
                throw new NotFoundException("Subsidiary not found!");

            return subsidiary;
        }

        public SubsidiaryDTO GetSubsidiaryByUser(UserDTO user) {
            
            switch (user.UserType) {
                case Domain.Enum.UserType.Subsidiary:
                    return this.Repository.GetByUser(user.Id);
                case Domain.Enum.UserType.Employee:
                    var employee = this._employeeRepository.GetByUserId(user.Id);
                    return employee.Subsidiary;
                default:
                    throw new System.Exception("Type of user invalid");
            }
        }
    }
}
