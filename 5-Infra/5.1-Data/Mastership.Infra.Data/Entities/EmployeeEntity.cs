using System;
using System.Collections.Generic;

namespace Mastership.Infra.Data.Entities
{
    public class EmployeeEntity : BaseEntity
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

        public Nullable<Guid> UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public Guid SubsidiaryId { get; set; }

        public virtual SubsidiaryEntity Subsidiary { get; set; }

        public virtual ICollection<PointTimeEntity> PointsTime { get; set; }
    }
}
