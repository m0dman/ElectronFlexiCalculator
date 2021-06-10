using RandoxITUtility.Application.Common.Mappings;
using RandoxITUtility.Domain.Entities;
using AutoMapper;
using System;

namespace RandoxITUtility.Application.Queries
{
    public class UpdateTestDTO : IMapFrom<Test>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}