using System;

namespace Mastership.Domain.ViewModels
{
    public class PointTimeViewModel : BaseViewModel
    {
        public DateTime DateTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IP { get; set; }
        public string UserHostName { get; set; }
        public Guid EmployeeId { get; set; }
        public Int64 Sequential { get; set; }
    }
}
