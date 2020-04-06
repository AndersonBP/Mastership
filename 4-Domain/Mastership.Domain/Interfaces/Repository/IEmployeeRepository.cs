using System;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface IEmployeeRepository : IRepository<EmployeeDTO> {
        EmployeeDTO GetByRegistrationAndDomainName(string registration, string domainName);
        EmployeeDTO GetByUserId(Guid id);
        EmployeeDTO GetByForeingId(string id);
    }
}
