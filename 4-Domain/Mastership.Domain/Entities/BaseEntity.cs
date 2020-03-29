using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
