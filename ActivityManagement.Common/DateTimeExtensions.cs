using System;
using System.Collections.Generic;
using MD.PersianDateTime.Core;

namespace ActivityManagement.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertPersianToGeorgian(this string date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(date);
            return persianDateTime.ToDateTime();
        }

        public static string ConvertGeorgianToPersian(this DateTime? date, string format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);
        }
        /// <summary>
        /// تشخیص سال کبیسه
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime? date)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.IsLeapYear;
        }
        /// <summary>
        /// تشخیص تاریخ فارسی
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTimeResult CheckPersianDateTime(this string date)
        {
            try
            {
                DateTime miladiDate = PersianDateTime.Parse(date).ToDateTime();
                return new DateTimeResult { GeorgianDate = miladiDate, IsPersian = true };
            }

            catch
            {
                return new DateTimeResult { IsPersian = false };
            }
        }

        public static DateTime DateTimeWithOutMilliseconds(DateTime dateTime) => dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));



    }

    public class DateTimeResult
    {
        public bool IsPersian { get; set; }
        public DateTime? GeorgianDate { get; set; }
    }

}
