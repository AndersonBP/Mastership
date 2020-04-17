using System;
using System.Collections.Generic;
using System.Net;

namespace Mastership.Domain.DTO
{
    public class CompanyIpRangesDTO : BaseDTO
    {
        public IPAddress Begin { get; set; }
        public IPAddress End { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }
    }
}
