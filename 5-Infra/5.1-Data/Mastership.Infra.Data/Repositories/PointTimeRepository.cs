using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Mastership.Domain.DTO;
using AutoMapper;
using Mastership.Infra.CrossCutting.Extensions;

namespace Mastership.Database.Repositories
{
    public class PointTimeRepository : BaseRepository<PointTimeDTO, PointTimeEntity>, IPointTimeRepository
    {
        public PointTimeRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public IQueryable<PointTimeDTO> GetByDay(DateTime day, Guid employeId)
        {
            return this._mapper.ProjectTo<PointTimeDTO>(this.Query().Where(x => x.EmployeeId.Equals(employeId) && x.DateTime.Date.Equals(day))); ;
        }

        public IEnumerable<PointTimeDTO> GetByRange(DateTime start, DateTime end, Guid subsidiary)
        {
            var query = this.Query(includeDefault: false).Where(x => x.DateTime >= start && x.DateTime<=end && x.Employee.SubsidiaryId.Equals(subsidiary)).OrderBy(x=>x.Sequential);
            return this.MapToDTO(query);
        }

        public long GetLastSequentialOf(Guid subsidiaryId)
        {
            var query = this.Query()
                .Where(x => x.Employee.SubsidiaryId == subsidiaryId)
                .OrderByDescending(x => x.Sequential)
                .Select(x => x.Sequential)
                .FirstOrDefault();

            return query;
        }

        public PointTimeDTO Search(Guid id, string domainName)
        {
            return this.MapToDTO(this.Query(includeDefault:true).FirstOrDefault(x => x.Id.Equals(id) && x.Employee.Subsidiary.Company.DomainName.Equals(domainName)));
        }
    }
}
