using System;
using System.Collections.Generic;
using System.Text;
using Mastership.Domain.Enum;

namespace Mastership.Domain.DTO
{
    public class UserDTO:BaseDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; }
        public ICollection<SubsidiaryDTO> Subsidiaries { get; set; }
    }
}
