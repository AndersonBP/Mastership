using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System.Security.Cryptography;

namespace Mastership.Application.Services
{
    public class UserApplication : BaseApplication<UserViewModel, UserDTO, IUserRepository>, IUserApplication
    {
        public UserApplication(IUserRepository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService) { }

        public UserViewModel Authenticate(string domain, LoginViewModel login)
        {
            MD5 md5Hash = MD5.Create();
            var password = MD5Extension.GetMd5Hash(md5Hash, login.Password);
            var user = this._repository.Authenticate(login.Username, login.Email, password, domain);
            return this.MapToViewModel(user);
        }
    }
}
