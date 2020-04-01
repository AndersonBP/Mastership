using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class PointTimeRepository : BaseRepository<PointTimeEntity>, IPointTimeRepository
    {
        public PointTimeRepository(IDataUnitOfWork uow) : base(uow) { }

        public IQueryable<PointTimeEntity> GetByDay(DateTime day, Guid employeId)
        {
            return this.Query().Where(x => x.EmployeeId.Equals(employeId) && x.Day.Equals(day));
        }
    }
}
