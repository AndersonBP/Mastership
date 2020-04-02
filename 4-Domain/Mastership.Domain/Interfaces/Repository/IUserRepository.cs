using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface IUserRepository : IRepository<UserDTO> { }
}
