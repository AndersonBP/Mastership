using System;

namespace Mastership.Domain.ViewModels
{
    public class CompanySettingsViewModel : BaseViewModel
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public Guid CompanyId { get; set; }
        public virtual CompanyViewModel Company { get; set; }
    }
}
