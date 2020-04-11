using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class EmployeeDTO: BaseDTO
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
        public Nullable<DateTime> DisabledDate { get; set; }
        public string ForeignId { get; set; }

        public Nullable<Guid> UserId { get; set; } = null;
        public virtual UserDTO User { get; set; }

        public Guid SubsidiaryId { get; set; }
        public virtual SubsidiaryDTO Subsidiary { get; set; }

        public virtual ICollection<PointTimeDTO> PointsTime { get; set; }
    }
}
