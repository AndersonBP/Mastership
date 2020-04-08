using System;

namespace Mastership.Infra.Data.Entities
{
    public class PointTimeEntity : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IP { get; set; }
        public string UserHostName { get; set; }
        public Guid EmployeeId { get; set; }
        public Int64 Sequential { get; set; }

        public virtual EmployeeEntity Employee { get; set; }
    }
}
