using System;
using System.Collections.Generic;
using MD.PersianDateTime.Core;

namespace ActivityManagement.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertShamsiToMiladi(this string date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(date);
            return persianDateTime.ToDateTime();
        }

        public static string ConvertMiladiToShamsi(this DateTime? date, string format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);
        }
        public static bool IsLeapYear(this DateTime? date)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.IsLeapYear;
        }
        public static DateTimeResult CheckShamsiDateTime(this string date)
        {
            try
            {
                DateTime miladiDate = PersianDateTime.Parse(date).ToDateTime();
                return new DateTimeResult { MiladiDate = miladiDate, IsShamsi = true };
            }

            catch
            {
                return new DateTimeResult { IsShamsi = false };
            }
        }

        public static DateTime DateTimeWithOutMilliseconds(DateTime dateTime) => dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));



    }

    public class DateTimeResult
    {
        public bool IsShamsi { get; set; }
        public DateTime? MiladiDate { get; set; }
    }

}
