using System;
using System.Globalization;

namespace ProyectoGenerico.Helper
{
    public static class DateTimeHelper
    {
        public static string DataTimeNullableToString(DateTime? value)
        {
            return value == null ?
                null :
                ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static bool IsValidDate(string value, string dateFormat)
        {
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
                return false;
        }

        public static DateTime StringToDateTimeNullable(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss.fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime StringToDateNullable(string value)
        {
            return DateTime.ParseExact(value, "yyyy/MM/dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public static String DateTimeToDateSap(DateTime? value)
        {
            return value == null ?
            null :
            ((DateTime)value).ToString("yyyyMMdd");
        }

        public static String StringToDateSap(string value)
        {
            return value.Replace("/", "");
        }

        public static String DateTimeToHourSap(DateTime? value)
        {
            return value == null ?
            null :
            ((DateTime)value).ToString("HH:mm:ss");
        }
    }
}