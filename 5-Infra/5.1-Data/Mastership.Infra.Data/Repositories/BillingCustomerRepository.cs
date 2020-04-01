using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class BillingCustomerRepository : BaseRepository<BillingCustomerEntity>, IBillingCustomerRepository
    {
        public BillingCustomerRepository(IDataUnitOfWork uow) : base(uow) { }

    }
}
