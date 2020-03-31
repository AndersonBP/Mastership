using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IDataUnitOfWork uow) : base(uow) { }

        public EmployeeEntity GetByRegistration(string registration)
        {
            return this.Query().Where(x => x.Registration.Equals(registration)).FirstOrDefault();
        }
    }
}
