using System;
using System.Collections.Generic;
using Mastership.Domain.Enum;

namespace Mastership.Infra.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

        public virtual ICollection<EmployeeEntity> Employees { get; set; }
        public virtual ICollection<SubsidiaryEntity> Subsidiaries { get; set; }
    }
}
