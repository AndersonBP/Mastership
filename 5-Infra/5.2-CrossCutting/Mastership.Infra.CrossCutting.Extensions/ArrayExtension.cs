using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class ArrayExtension
    {
        public static T[] RemoveFromArray<T>(this T[] original, T itemToRemove)
        {
            int numIdx = System.Array.IndexOf(original, itemToRemove);
            if (numIdx == -1) return original;
            List<T> tmp = new List<T>(original);
            tmp.RemoveAt(numIdx);
            return tmp.ToArray();
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
    }
}
