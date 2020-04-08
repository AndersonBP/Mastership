using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface ICompanyRepository : IRepository<CompanyDTO> {
        CompanyDTO GetByDomainName(string domainName);
    }
}
