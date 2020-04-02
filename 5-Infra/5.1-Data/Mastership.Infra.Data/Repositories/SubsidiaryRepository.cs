using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class SubsidiaryRepository : BaseRepository<SubsidiaryDTO, SubsidiaryEntity>, ISubsidiaryRepository
    {
        public SubsidiaryRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public SubsidiaryDTO GetByDomainName(string domainName)
        {
            return this._mapper.Map<SubsidiaryDTO>(this.Query().Where(x => x.DomainName.Equals(domainName)).FirstOrDefault());
        }
    }
}
