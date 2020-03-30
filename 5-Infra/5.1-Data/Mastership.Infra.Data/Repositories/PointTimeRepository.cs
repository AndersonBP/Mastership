using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class PointTimeRepository : BaseRepository<PointTimeEntity>, IPointTimeRepository
    {
        public PointTimeRepository(IDataUnitOfWork uow) : base(uow) { }

    }
}
