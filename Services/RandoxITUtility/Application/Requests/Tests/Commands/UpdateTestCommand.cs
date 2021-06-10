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
    public class UpdateTestCommand : IRequest<bool>
    {
        public UpdateTestDTO Test { get; set; }
    }

    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, bool>
    {
        private readonly RandoxITUtilityContext _Context;
        private readonly IMapper _Mapper;

        public UpdateTestCommandHandler(RandoxITUtilityContext context, IMapper mapper)
        {
            _Context = context;
            _Mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var existingTest = _Context.Tests.Find(request.Test.ID);
            if (existingTest != null)
            {
                _Context.Entry(existingTest).CurrentValues.SetValues(request.Test);
            }
            try
            {
                return (await _Context.SaveChangesAsync()) > 0;
            }
            catch (DbUpdateException /* ex */)
            {
                return false;
            }
        }
    }
}