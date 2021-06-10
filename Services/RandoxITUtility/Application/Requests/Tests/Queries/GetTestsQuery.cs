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

namespace RandoxITUtility.Application.Queries
{
    public class GetTestsQuery : IRequest<IList<TestDTO>>
    {
    }

    public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, IList<TestDTO>>
    {
        private readonly RandoxITUtilityContext _context;
        private readonly IMapper _mapper;

        public GetTestsQueryHandler(RandoxITUtilityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<TestDTO>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tests
                .ProjectTo<TestDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken);
        }
    }
}