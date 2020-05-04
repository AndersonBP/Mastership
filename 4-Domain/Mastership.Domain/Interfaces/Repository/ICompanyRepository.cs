using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Mastership.Domain.Repository
{
    public interface ICompanyRepository : IRepository<CompanyDTO> {
        CompanyDTO GetByDomainName(string domainName);
        IEnumerable<CompanyDTO> WithAfdScheduled();
    }
}
