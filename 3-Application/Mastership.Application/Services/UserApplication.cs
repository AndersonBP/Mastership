using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class UserApplication : BaseApplication<UserViewModel, UserEntity, IUserRepository>, IUserApplication
    {
        public UserApplication(IUserRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
