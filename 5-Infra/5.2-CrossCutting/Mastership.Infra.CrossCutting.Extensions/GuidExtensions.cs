using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid source)
        {
            return source == null || source == Guid.Empty;
        }

    }
}
