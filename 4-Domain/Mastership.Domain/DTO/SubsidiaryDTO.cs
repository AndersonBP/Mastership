using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class SubsidiaryDTO:BaseDTO
    {
        public string Name { get; set; }

        public string ForeignId { get; set; }
        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }
        public string CEI { get; set; }
        public string REP { get; set; }

        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Guid UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public Guid CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; }
    }
}
