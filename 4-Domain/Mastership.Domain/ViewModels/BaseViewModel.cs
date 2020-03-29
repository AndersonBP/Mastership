using System;

namespace Mastership.Domain.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
