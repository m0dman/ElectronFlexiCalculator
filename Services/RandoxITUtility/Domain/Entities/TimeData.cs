using System;
using RandoxITUtility.Domain;

namespace RandoxITUtility.Domain.Entities
{
    public class TimeData
    {
        public Guid Id { get; set; }
        public DateTime WeekCommencing { get; set; } 
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public DateTimeOffset LunchLength { get; set; }
        public string DayOfTheWeek { get; set; }
    }
}
