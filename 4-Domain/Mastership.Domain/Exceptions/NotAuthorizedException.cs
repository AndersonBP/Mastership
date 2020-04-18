using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.Exceptions
{
    public class NetworkException : Exception
    {
        public NetworkException(string message) : base(message) { }
    }
}
