using System;
using System.Collections.Generic;

namespace Mastership.Domain.DTO
{
    public class CompanySettingsDTO : BaseDTO
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public bool AFDScheduled { get; set; }
        public string FTPHost { get; set; }
        public string FTPPass { get; set; }
        public string FTPUser { get; set; }
        public string FTPPath { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }
    }
}
