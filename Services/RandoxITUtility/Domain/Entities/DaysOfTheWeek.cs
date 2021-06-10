using System;
using RandoxITUtility.Domain;

namespace RandoxITUtility.Domain.Entities
{
    public class DaysOfTheWeek
    {
        public Guid? Id { get; set; } 
        public string DayOfTheWeek { get; set; }
        public DateTime LunchLength { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
