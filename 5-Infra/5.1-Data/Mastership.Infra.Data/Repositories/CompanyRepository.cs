using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyDTO, CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) {

        }

        public CompanyDTO GetByDomainName(string domainName)
        {
            return this._mapper.Map<CompanyDTO>(this.Query().Where(x => x.DomainName.Equals(domainName)).FirstOrDefault());
        }

    }
}
