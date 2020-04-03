using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System.Security.Cryptography;

namespace Mastership.Application.Services
{
    public class UserApplication : BaseApplication<UserViewModel, UserDTO, IUserRepository>, IUserApplication
    {
        public UserApplication(IUserRepository repository, IMapper mapper) : base(repository, mapper) { }

        public UserViewModel Authenticate(LoginViewModel login)
        {
            MD5 md5Hash = MD5.Create();
            var senha = MD5Extension.GetMd5Hash(md5Hash, login.password);
            return this.MapToViewModel(this.Repository.Authenticate(login.user, senha, login.domain));
        }
    }
}
