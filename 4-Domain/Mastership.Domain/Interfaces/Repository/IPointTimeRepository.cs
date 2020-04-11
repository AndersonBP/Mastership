using Mastership.Domain.Interfaces.Repository;
using System;
using System.Linq;
using Mastership.Domain.DTO;
using System.Collections.Generic;

namespace Mastership.Domain.Repository
{
    public interface IPointTimeRepository : IRepository<PointTimeDTO> {
        IQueryable<PointTimeDTO> GetByDay(DateTime day, Guid employeId);
        IEnumerable<PointTimeDTO> GetByRange(DateTime start, DateTime end, Guid subsidiary);
        long GetLastSequentialOf(Guid subsidiaryId);
        PointTimeDTO Search(Guid id, string domainName);
    }
}
