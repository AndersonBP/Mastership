using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mastership.Domain.Repository
{
    public interface ICompanyIpRangesRepository : IRepository<CompanyIpRangesDTO> {
        ICollection<CompanyIpRangesDTO> GetByCompany(Guid company);
    }
}
