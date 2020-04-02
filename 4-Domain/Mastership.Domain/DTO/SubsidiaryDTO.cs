using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class SubsidiaryDTO:BaseDTO
    {
        public string Name { get; set; }

        public string ForeignId { get; set; }


        public Guid CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; }
    }
}
