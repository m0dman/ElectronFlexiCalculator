using RandoxITUtility.Application.Common.Mappings;
using RandoxITUtility.Domain.Entities;
using AutoMapper;
using System;

namespace RandoxITUtility.Application.Queries.GetTests
{
    public class TestDTO : IMapFrom<Test>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}