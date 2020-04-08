﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.DTO
{
    public class PointTimeDTO:BaseDTO
    {
        public DateTime DateTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IP { get; set; }
        public string UserHostName { get; set; }
        public Guid EmployeeId { get; set; }
        public Int64 Sequential { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
    }
}
