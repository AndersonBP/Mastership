using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeDTO, EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public EmployeeDTO GetByForeingId(string id)
        {
            return this._mapper.Map<EmployeeDTO>(this.Query(includes: false).FirstOrDefault(x => x.ForeignId.Equals(id)));
        }

        public EmployeeDTO GetByRegistrationAndDomainName(string registration, string domainName)
        {
            return this._mapper.Map<EmployeeDTO>(this.Query().Where(x => x.Registration.Equals(registration) && x.Subsidiary.DomainName.Equals(domainName) && !x.Deleted && x.Enable).FirstOrDefault());
        }
        public EmployeeDTO GetByUserId(Guid id)
        {
            var query = this.Query().FirstOrDefault(x => x.UserId == id);
            return this._mapper.Map<EmployeeDTO>(query);
        }
        public override IQueryable<EmployeeEntity> Includes(IQueryable<EmployeeEntity> query)
        {
            return query.Include(i => i.Subsidiary).Include(i => i.PointsTime);
        }
    }
}
