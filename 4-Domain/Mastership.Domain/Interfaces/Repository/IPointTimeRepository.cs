using Mastership.Domain.Interfaces.Repository;
using System;
using System.Linq;
using Mastership.Domain.DTO;

namespace Mastership.Domain.Repository
{
    public interface IPointTimeRepository : IRepository<PointTimeDTO> {
        IQueryable<PointTimeDTO> GetByDay(DateTime day, Guid employeId);
    }
}
