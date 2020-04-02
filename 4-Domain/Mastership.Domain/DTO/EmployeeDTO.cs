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
        public DateTime AdmissionDate { get; set; }
        public DateTime Birthday { get; set; }

        public string ForeignId { get; set; }

        public Guid SubsidiaryId { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual SubsidiaryDTO Subsidiary { get; set; }

        public virtual ICollection<PointTimeDTO> PointsTime { get; set; }
    }
}
