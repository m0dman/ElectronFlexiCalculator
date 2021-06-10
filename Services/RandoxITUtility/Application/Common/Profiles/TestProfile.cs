using RandoxITUtility.Application.Queries;
using RandoxITUtility.Domain.Entities;
using AutoMapper;
using RandoxITUtility.Application.Queries.GetTests;

namespace RandoxITUtility.Application.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            // Default mapping when property names are same
            CreateMap<Test, TestDTO>();
            CreateMap<CreateTestDTO, Test>();
            CreateMap<UpdateTestDTO, Test>();
        }
    }
}