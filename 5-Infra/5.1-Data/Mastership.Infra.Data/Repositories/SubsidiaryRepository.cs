using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class SubsidiaryRepository : BaseRepository<SubsidiaryEntity>, ISubsidiaryRepository
    {
        public SubsidiaryRepository(IDataUnitOfWork uow) : base(uow) { }

    }
}
