using System;
using System.Collections.Generic;

namespace Mastership.Infra.Data.Entities
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

        public string ForeignId { get; set; }
        
        public Guid BillingCustomerId { get; set; }
        public virtual BillingCustomerEntity BillingCustomer { get; set; }

        public ICollection<SubsidiaryEntity> Subsidiaries { get; set; }
    }
}
