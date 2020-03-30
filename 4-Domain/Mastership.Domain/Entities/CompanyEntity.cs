using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string DomainName { get; set; }
        
        public string CNPJ { get; set; }

        public ICollection<EmployeeEntity> Employees { get; set; }

    }
}
