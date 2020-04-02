using System;
using System.Collections.Generic;

namespace Mastership.Domain.ViewModels
{
    public class BillingCustomerViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public ICollection<CompanyViewModel> Companies { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}
