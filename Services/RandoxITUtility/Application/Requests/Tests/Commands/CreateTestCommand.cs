using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RandoxITUtility.Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RandoxITUtility.Infrastructure.Data.Contexts;
using RandoxITUtility.Application.Queries.GetTests;
using RandoxITUtility.Domain.Entities;
using RandoxITUtility.Application.Profiles;
using System;

namespace RandoxITUtility.Application.Queries
{

    public class CreateTestCommand : IRequest<Guid>
    {
        public CreateTestDTO Test { get; set; }
    }

    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, Guid>
    {
        private readonly RandoxITUtilityContext _context;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(RandoxITUtilityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var mappedRecord = _mapper.Map<Test>(request.Test);
            _context.Tests.Add(mappedRecord);
            await _context.SaveChangesAsync();

            return mappedRecord.Id;
        }
    }
}