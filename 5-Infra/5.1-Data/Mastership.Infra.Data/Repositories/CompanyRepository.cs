using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyDTO, CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) {

        }

        public CompanyDTO GetByDomainName(string domainName)
        {
            return this.MapToDTO(this.Query(includeDefault: false).Include(x => x.Settings).Where(x => x.DomainName.Equals(domainName)).FirstOrDefault());
        }

        public IEnumerable<CompanyDTO> WithAfdScheduled()
        {
            return this.MapToDTO(this.Query(includeDefault: false).Include(x => x.Settings).Include(x=>x.Subsidiaries).Where(x => x.Settings.AFDScheduled)).ToList();
        }
    }
}
