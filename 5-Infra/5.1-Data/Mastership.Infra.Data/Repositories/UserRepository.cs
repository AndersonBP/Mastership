using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class UserRepository : BaseRepository<UserDTO, UserEntity>, IUserRepository
    {
        public UserRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public UserDTO Authenticate(string username, string password, string domain)
        {
            return this._mapper.Map<UserDTO>(this.Query().Where(x => x.Username.ToLower().Equals(username.ToLower()) && x.Password.Equals(password)
             && (x.Employee.Subsidiary.DomainName.ToLower().Equals(domain.ToLower()))).FirstOrDefault());
        }
    }
}
