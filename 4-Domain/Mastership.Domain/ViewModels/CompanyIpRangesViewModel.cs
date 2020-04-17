using System;
using System.Net;

namespace Mastership.Domain.ViewModels
{
    public class CompanyIpRangesViewModel : BaseViewModel
    {
        public IPAddress Begin { get; set; }
        public IPAddress End { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyViewModel Company { get; set; }

    }
}
