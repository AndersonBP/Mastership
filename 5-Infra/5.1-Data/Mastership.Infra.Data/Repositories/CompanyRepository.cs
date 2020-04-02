using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyDTO, CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

    }
}
