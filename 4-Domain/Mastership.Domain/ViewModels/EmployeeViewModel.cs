using System;
using System.Collections.Generic;

namespace Mastership.Domain.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string Registration { get; set; }
        public string PIS { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime Birthday { get; set; }
        public Guid CompanyId { get; set; }
        public virtual ICollection<PointTimeViewModel> PointsTime { get; set; }
        public virtual CompanyViewModel Company { get; set; }
    }
}
