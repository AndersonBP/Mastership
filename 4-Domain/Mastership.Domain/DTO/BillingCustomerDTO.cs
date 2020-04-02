using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class BillingCustomerDTO:BaseDTO
    {
        public string Name { get; set; }
        public ICollection<CompanyDTO> Companies { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
