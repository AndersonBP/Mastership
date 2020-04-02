using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;

namespace Mastership.Database.Repositories
{
    public class UserRepository : BaseRepository<UserDTO, UserEntity>, IUserRepository
    {
        public UserRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

    }
}
