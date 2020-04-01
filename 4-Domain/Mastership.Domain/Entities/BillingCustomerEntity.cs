using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class BillingCustomerEntity : BaseEntity
    {
        public ICollection<CompanyEntity> Companies { get; set; }

    }
}
