using System;

namespace Mastership.Infra.Data.Entities
{
    public class CompanySettingsEntity : BaseEntity
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }
    }
}
