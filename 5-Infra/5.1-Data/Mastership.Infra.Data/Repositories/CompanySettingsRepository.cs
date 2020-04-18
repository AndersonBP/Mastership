using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class CompanySettingsRepository : BaseRepository<CompanySettingsDTO, CompanySettingsEntity>, ICompanySettingsRepository
    {
        public CompanySettingsRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

    }
}
