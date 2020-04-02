using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.Exceptions
{
    public class ExceptionResponseViewModel
    {
        public Guid RequestIdentity { get; set; }

        public string Message { get; set; }
    }
}
