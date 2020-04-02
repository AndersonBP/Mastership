using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class UserDTO:BaseDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
        public Nullable<Guid> EmployeeId { get; set; }
        public virtual BillingCustomerDTO BillingCustomer { get; set; }
        public Nullable<Guid> BillingCustomerId { get; set; }
    }
}
