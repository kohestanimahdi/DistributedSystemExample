using System;

namespace DistributedSystems.Web.Utilities
{
    public static class DateTimeHelpers
    {
        private static System.Globalization.PersianCalendar persianCalendar = new();

        public static string ConvertToPersionTime(DateTime dateTime)
        => $"{persianCalendar.GetHour(dateTime)}:{persianCalendar.GetMinute(dateTime)}";

        public static string ConvertToPersionDate(DateTime dateTime)
       => $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime)}/{persianCalendar.GetDayOfMonth(dateTime)} ";


        public static string ConvertToPersionTime(string dateTime)
        => ConvertToPersionTime(Convert.ToDateTime(dateTime));

        public static string ConvertToPersionDate(string dateTime)
        => ConvertToPersionDate(Convert.ToDateTime(dateTime));

    }
}
