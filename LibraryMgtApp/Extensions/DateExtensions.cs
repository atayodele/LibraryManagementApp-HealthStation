using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions
{
    public static class DateExtensions
    {
        public static DateTime GetDateUtcNow(this DateTime now)
        {
            return DateTime.UtcNow;
        }

        public static DateTime FindNextDate(this DateTime startDate, int interval)
        {
            DateTime today = DateTime.Today;
            do
            {
                startDate = startDate.AddMonths(interval);
            } while (startDate <= today);
            return startDate;
        }


        public static DateTime ToInvariantDateTime(this String value, String format)
        {
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.InvariantInfo;
            var result = DateTime.TryParseExact(value, format, dtfi, DateTimeStyles.None, out DateTime newValue);
            return newValue;
        }

        public static string ToDateString(this DateTime dt, String format)
        {
            return dt.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }
    }
}
