using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RandoxITUtility.Infrastructure.Data.Contexts;

namespace RandoxITUtility.Application.Queries.GetDaysOfTheWeek
{
    public class GetDaysOfTheWeekQuery : IRequest<List<TimeDataDTO>>
    {
        public DateTime WeekCommencing { get; set; }

        public GetDaysOfTheWeekQuery(DateTime weekCommencing)
        {
            WeekCommencing = weekCommencing;
        }
    }

    public class GetDaysOfTheWeekQueryHandler : IRequestHandler<GetDaysOfTheWeekQuery, List<TimeDataDTO>>
    {
        private readonly RandoxITUtilityContext _context;
        private readonly IMapper _mapper;

        public GetDaysOfTheWeekQueryHandler(RandoxITUtilityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TimeDataDTO>> Handle(GetDaysOfTheWeekQuery request, CancellationToken cancellationToken)
        {
            return await _context.WeeklyTimes
                .ProjectTo<TimeDataDTO>(_mapper.ConfigurationProvider)
                .Where(x => x.WeekCommencing == request.WeekCommencing)
                .ToListAsync();
        }
    }
}