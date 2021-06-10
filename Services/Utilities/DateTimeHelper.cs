using System;

namespace RandoxITUtility.Utilities
{
    public static class DateTimeHelpers
    {
        public static bool Between(this DateTime dateTime, DateTime startDate, DateTime endDate)
        {
            return dateTime >= startDate && dateTime <= endDate;
        }
    }
}