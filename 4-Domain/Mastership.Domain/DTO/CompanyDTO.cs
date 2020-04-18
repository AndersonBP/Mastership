using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class CompanyDTO: BaseDTO
    {
        public string Name { get; set; }
        public string RazaoSocial { get; set; }

        public string DomainName { get; set; }
        public string CNPJ { get; set; }

        public string Adress { get; set; }
        public string Image { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ForeignId { get; set; }

        public Guid BillingCustomerId { get; set; }
        public virtual BillingCustomerDTO BillingCustomer { get; set; }

        public ICollection<SubsidiaryDTO> Subsidiaries { get; set; }
        public ICollection<CompanyIpRangesDTO> IpRanges { get; set; }
        public virtual CompanySettingsDTO Settings { get; set; }

    }
}
