using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class CompanyIpRangesRepository : BaseRepository<CompanyIpRangesDTO, CompanyIpRangesEntity>, ICompanyIpRangesRepository
    {
        public CompanyIpRangesRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

    }
}
