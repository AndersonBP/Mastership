using System;
using System.Collections.Generic;

namespace Mastership.Domain.DTO
{
    public class CompanySettingsDTO : BaseDTO
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }
    }
}
