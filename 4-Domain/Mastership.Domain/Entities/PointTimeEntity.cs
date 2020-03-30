using System;

namespace Mastership.Domain.Entities
{
    public class PointTimeEntity : BaseEntity
    {
        public DateTime Hour { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IP { get; set; }
        public string UserHostName { get; set; }
    }
}
