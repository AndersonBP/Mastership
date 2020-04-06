using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo FindProperty(this object obj, string[] names)
        {
            var typ = obj.GetType().GetProperties().Where(res => names.Contains(res.Name)).FirstOrDefault();
            return typ;
        }
    }
}
