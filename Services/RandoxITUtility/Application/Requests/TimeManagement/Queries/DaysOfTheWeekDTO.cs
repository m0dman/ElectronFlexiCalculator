using RandoxITUtility.Application.Common.Mappings;
using RandoxITUtility.Domain.Entities;
using AutoMapper;
using System;

namespace RandoxITUtility.Application.Queries.GetDaysOfTheWeek
{
    public class TimeDataDTO : IMapFrom<TimeData>
    {
        public Guid Id { get; set; } 
        public DateTime WeekCommencing { get; set; }
        public string DayOfTheWeek { get; set; }
        public DateTimeOffset LunchLength { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}