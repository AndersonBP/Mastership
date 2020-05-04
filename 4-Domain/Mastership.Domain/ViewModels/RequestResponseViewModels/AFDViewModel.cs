using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.ViewModels.RequestResponseViewModels
{
    public class AFDViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid Subsidiary { get; set; } = Guid.Empty;
    }
}
