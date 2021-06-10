using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace RandoxITUtility.Application.Behaviours
{
    /// <summary>
    /// Custom pipeline behaviour dedicated to DB Insert and Update failure operations
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class DBUpdateExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public DBUpdateExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (DbUpdateException ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "API: DbUpdate Exception for Request {Name} {@Request}", requestName, request);
                //probably should propogate the error higher so that it can be properly reported in some way to the client!
                throw;
            }
        }
    }
}