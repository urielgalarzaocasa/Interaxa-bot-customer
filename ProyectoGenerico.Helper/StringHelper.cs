using System;

namespace ProyectoGenerico.Helper
{
    public static class StringHelper
    {
        public static string Left(string param, int maxLength)
        {
            if (string.IsNullOrEmpty(param)) return param;
            maxLength = Math.Abs(maxLength);

            return (param.Length <= maxLength
                   ? param
                   : param.Substring(0, maxLength)
                   );
        }

        public static string Right(string param, int length)
        {
            int pos = param.Length - length;
            string result = param.Substring(pos, length);
            return result;
        }

        public static double ToDoubleOrDefault(this string value, double defaultValue = 0.0)
        {
            Double.TryParse(value, out defaultValue);
            return defaultValue;
        }

        public static int ToIntOrDefault(this string value, int defaultValue = 0)
        {
            Int32.TryParse(value, out defaultValue);
            return defaultValue;
        }
    }
}