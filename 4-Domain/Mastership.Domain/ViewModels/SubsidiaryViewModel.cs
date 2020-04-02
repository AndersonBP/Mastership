using System;
using System.Collections.Generic;

namespace Mastership.Domain.ViewModels
{
    public class SubsidiaryViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string ForeignId { get; set; }

        public Guid CompanyId { get; set; }
        public virtual CompanyViewModel Company { get; set; }

        public ICollection<EmployeeViewModel> Employees { get; set; }
    }
}
