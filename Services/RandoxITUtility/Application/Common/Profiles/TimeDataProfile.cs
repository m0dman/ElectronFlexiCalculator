using RandoxITUtility.Domain.Entities;
using AutoMapper;
using RandoxITUtility.Application.Queries.GetDaysOfTheWeek;

namespace RandoxITUtility.Application.Profiles
{
    public class TimeDataProfile : Profile
    {
        public TimeDataProfile()
        {
            // Default mapping when property names are same
            CreateMap<TimeData, TimeDataDTO>();
        }
    }
}