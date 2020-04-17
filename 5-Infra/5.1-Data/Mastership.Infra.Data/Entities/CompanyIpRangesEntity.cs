using System;
using System.Net;

namespace Mastership.Infra.Data.Entities
{
    public class CompanyIpRangesEntity : BaseEntity
    {
        public IPAddress Begin { get; set; }
        public IPAddress End { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

    }
}
