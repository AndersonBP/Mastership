using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Mastership.Domain.DTO;
using AutoMapper;

namespace Mastership.Database.Repositories
{
    public class PointTimeRepository : BaseRepository<PointTimeDTO, PointTimeEntity>, IPointTimeRepository
    {
        public PointTimeRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public IQueryable<PointTimeDTO> GetByDay(DateTime day, Guid employeId)
        {
            return this._mapper.ProjectTo<PointTimeDTO>(this.Query().Where(x => x.EmployeeId.Equals(employeId) && x.Day.Equals(day)));
        }
    }
}
