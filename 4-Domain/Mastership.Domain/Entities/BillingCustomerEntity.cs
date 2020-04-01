using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class BillingCustomerEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CompanyEntity> Companies { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
