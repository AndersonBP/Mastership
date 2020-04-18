using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.ViewModels
{
    public class CheckDomainNameViewModel
    {
        public string Name { get; set; }
        public string RazaoSocial { get; set; }
        public string DomainName { get; set; }

        public string CNPJ { get; set; }

        public string Adress { get; set; }
        public string Image { get; set; }
        public string ZipCode { get; set; }
        public bool Enable { get; set; }
        public DateTime ServerTime { get; set; }
    }
}
