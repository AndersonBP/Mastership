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
        public string RG { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string ForeignId { get; set; }
        public Nullable<Guid> UserId { get; set; } = null;

        public string CPFNumbers
        {
            get
            {
                return this.CPF.Replace(".", "").Replace("-", "");
            }
        }

        public Guid SubsidiaryId { get; set; }
        public virtual ICollection<PointTimeViewModel> PointsTime { get; set; }
        public virtual SubsidiaryViewModel Subsidiary { get; set; }
    }
}
