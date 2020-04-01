using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class SubsidiaryEntity : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

        public ICollection<EmployeeEntity> Employees { get; set; }

    }
}
