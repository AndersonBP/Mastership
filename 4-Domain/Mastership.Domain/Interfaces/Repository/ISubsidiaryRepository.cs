using System;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface ISubsidiaryRepository : IRepository<SubsidiaryDTO> {
        SubsidiaryDTO GetByDomainName(string domainName);
        SubsidiaryDTO GetByUser(Guid id);
    }
}
