using System;
using System.Collections.Generic;

namespace ProyectoGenerico.Helper
{
    public static class ObjectHelper
    {
        public static bool IsNullOrZero(this int? obj)
        {
            return obj == null && obj == 0;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static bool HasCount(this List<string> obj)
        {
            return obj.Count > 0;
        }

        public static bool HasCount(this Dictionary<string, string> obj)
        {
            return obj.Count > 0;
        }

        public static bool IsNullOrEmpty(this string obj)
        {
            return String.IsNullOrEmpty(obj);
        }
    }
}