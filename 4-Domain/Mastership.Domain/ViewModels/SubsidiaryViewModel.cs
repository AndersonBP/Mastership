using System;
using System.Collections.Generic;

namespace Mastership.Domain.ViewModels
{
    public class SubsidiaryViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string ForeignId { get; set; }
        public string DomainName { get; set; }
        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }

        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Guid CompanyId { get; set; }
        public virtual CompanyViewModel Company { get; set; }

        public ICollection<EmployeeViewModel> Employees { get; set; }
    }
}
