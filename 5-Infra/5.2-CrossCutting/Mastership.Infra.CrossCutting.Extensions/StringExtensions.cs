using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class StringExtensions
    {

        public static decimal ToDecimalOrZero(this string value)
            => string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value.Replace('.', ','), new NumberFormatInfo { NumberDecimalSeparator = "," });

        public static decimal? ToDecimalOrNull(this string value)
            => string.IsNullOrEmpty(value) ? null : (decimal?)decimal.Parse(value.Replace('.', ','), new NumberFormatInfo { NumberDecimalSeparator = "," });

        public static string ToNullOrTrim(this string value)
            => string.IsNullOrEmpty(value) ? null : value.Trim();

        public static string RemoveSpecialCharacters(this string str)
        {
            //return Regex.Replace(str, @"[^\w\.@-]/g", "", RegexOptions.None);
            return Regex.Replace(str, @"[^0-9a-zA-Z]+", "");
        }

    }
}
