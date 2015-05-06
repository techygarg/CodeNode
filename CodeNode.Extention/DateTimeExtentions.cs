using System;
using System.Collections.Generic;

namespace CodeNode.Extention
{
    public static class DateTimeExtentions
    {
        public static DateTime BeginningOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static bool IsLeapDay(this DateTime date)
        {
            return (date.Month == 2 && date.Day == 29);
        }

        public static TimeSpan TimeElapsed(this DateTime date)
        {
            return DateTime.Now - date;
        }

        public static DateTime GetFirstDayOfNextMonth(this DateTime date)
        {
            var dateTo = date;
            dateTo = dateTo.AddMonths(1);
            return dateTo.BeginningOfTheMonth();
        }

        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            var dateTo = date;
            dateTo = dateTo.AddMonths(1);
            dateTo = dateTo.AddDays(-dateTo.Day);
            return dateTo.Date;
        }

        public static IList<DateTime> GetMonthsBetweenDates(this DateTime startDate, DateTime endDate)
        {
            var monthList = new List<DateTime>();
            var currentDate = startDate;
            while (currentDate >= startDate && currentDate <= endDate)
            {
                monthList.Add(currentDate.BeginningOfTheMonth());
                currentDate = currentDate.GetFirstDayOfNextMonth();
            }

            return monthList;
        }

        public static int GetDaysBetweenDates(this DateTime startDate, DateTime endDate)
        {
            return endDate.Subtract(startDate).Days + 1;
        }

        public static int GetTotalHoursInMonth(this DateTime dateTime)
        {
            var firstDate = dateTime.BeginningOfTheMonth();
            var lastDate = dateTime.BeginningOfTheMonth();
            return firstDate.GetDaysBetweenDates(lastDate)*24;
        }
    }
}