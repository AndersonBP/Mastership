using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeDTO, EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public EmployeeDTO GetByRegistrationAndDomainName(string registration, string domainName)
        {
            return  this._mapper.Map<EmployeeDTO>(this.Query().Where(x => x.Registration.Equals(registration) && x.Subsidiary.DomainName.Equals(domainName)).FirstOrDefault());
        }
    }
}
