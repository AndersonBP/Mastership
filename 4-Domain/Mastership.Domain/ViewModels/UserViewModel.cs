using System;
using Mastership.Domain.Enum;

namespace Mastership.Domain.ViewModels
{
    public class UserViewModel : BaseViewModel {
        public UserType UserType { get; set; }
    }
}
