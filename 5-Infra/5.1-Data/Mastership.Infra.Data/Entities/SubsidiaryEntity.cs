using System;
using System.Collections.Generic;

namespace Mastership.Infra.Data.Entities
{
    public class SubsidiaryEntity : BaseEntity
    {
        public string Name { get; set; }

        public string ForeignId { get; set; }
        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }
        public string CEI { get; set; }

        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Nullable<Guid> UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

        public ICollection<EmployeeEntity> Employees { get; set; }

    }
}
