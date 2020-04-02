using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface IEmployeeRepository : IRepository<EmployeeDTO> {
        EmployeeDTO GetByRegistration(string registration);
    }
}
