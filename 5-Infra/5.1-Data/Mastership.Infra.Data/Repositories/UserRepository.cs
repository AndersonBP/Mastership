using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IDataUnitOfWork uow) : base(uow) { }

    }
}
