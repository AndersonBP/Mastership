using System;

namespace Mastership.Infra.Data.Entities
{
    public class CompanySettingsEntity : BaseEntity
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public bool AFDScheduled { get; set; }
        public string FTPHost { get; set; }
        public string FTPPass { get; set; }
        public string FTPUser { get; set; }
        public string FTPPath { get; set; }


        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }
    }
}
