using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class IPAddressExtensions
    {
        public static uint ToInteger(this IPAddress ipAddress)
        {
            byte[] bytes = ipAddress.GetAddressBytes();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

    }
}
