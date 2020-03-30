using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Registration { get; set; }
        public Guid CompanyId { get; set; }

        public virtual ICollection<PointTimeEntity> PointsTime { get; set; }
        public virtual CompanyEntity Company { get; set; }
    }
}
