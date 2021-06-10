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
using System;

namespace RandoxITUtility.Application.Queries
{
    public class GetTestQuery : IRequest<TestDTO>
    {
        public Guid ID { get; set; }
    }

    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, TestDTO>
    {
        private readonly RandoxITUtilityContext _context;
        private readonly IMapper _mapper;

        public GetTestQueryHandler(RandoxITUtilityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestDTO> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tests
                .ProjectTo<TestDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == request.ID);
        }
    }
}