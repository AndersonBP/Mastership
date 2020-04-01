using System;

namespace Mastership.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual EmployeeEntity Employee { get; set; }
        public Nullable<Guid> EmployeeId { get; set; }
        public virtual BillingCustomerEntity BillingCustomer { get; set; }
        public Nullable<Guid> BillingCustomerId { get; set; }
    }
}
