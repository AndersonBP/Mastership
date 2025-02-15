using System;
using System.Collections.Generic;

namespace Mastership.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public string RazaoSocial { get; set; }

        public string DomainName { get; set; }
        public string CNPJ { get; set; }

        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<EmployeeEntity> Employees { get; set; }

    }
}
