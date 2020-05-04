using System;

namespace Mastership.Domain.ViewModels
{
    public class CompanySettingsViewModel : BaseViewModel
    {
        public bool UseIpFilter { get; set; }
        public bool AllowMobile { get; set; }
        public Guid CompanyId { get; set; }
        public bool AFDScheduled { get; set; }
        public string FTPHost { get; set; }
        public string FTPPass { get; set; }
        public string FTPUser { get; set; }
        public string FTPPath { get; set; }
        public virtual CompanyViewModel Company { get; set; }
    }
}
