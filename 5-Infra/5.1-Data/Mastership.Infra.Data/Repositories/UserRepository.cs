using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;
using Mastership.Domain.Enum;

namespace Mastership.Database.Repositories
{
    public class UserRepository : BaseRepository<UserDTO, UserEntity>, IUserRepository
    {
        public UserRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public UserDTO Authenticate(string username, string email, string password, string domain)
        {
            var query = this.Query()
                .Where(x => x.UserType == UserType.Employee ?
                    x.Employees.FirstOrDefault().Subsidiary.Company.DomainName.Equals(domain) : x.Subsidiaries.FirstOrDefault().Company.DomainName.Equals(domain))
                .Where(x =>
                    (x.Username.ToLower().Equals(username.ToLower()) || x.Email.ToLower().Equals(email.ToLower()))
                    && x.Password.Equals(password));

            return this._mapper.Map<UserDTO>(query.FirstOrDefault());
        }
    }
}
