using System;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class DecimalExtensions
    {

        public static decimal ToFixed(this decimal value, int places)
            => Math.Round(value, places);

        public static decimal? ToFixed(this decimal? value, int places)
        {
            if (!value.HasValue)
                return null;

            return Math.Round(value.Value, places);
        }

        public static decimal ToPositive(this decimal value)
            => value < 0 ? value * -1 : value;
    }
}