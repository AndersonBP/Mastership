using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Repository;
using System;
using System.Linq;

namespace Mastership.Domain.Repository
{
    public interface IPointTimeRepository : IRepository<PointTimeEntity> {
        IQueryable<PointTimeEntity> GetByDay(DateTime day, Guid employeId);
    }
}
