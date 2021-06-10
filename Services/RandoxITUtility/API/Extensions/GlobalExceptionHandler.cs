

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RandoxITUtility.API.Middleware;
using RandoxITUtilityAPI.Models;

namespace RandoxITUtility.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class GlobalExceptionHandlerExtension
    {
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
           {
               appError.Run(async context =>
               {
                   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                   context.Response.ContentType = "application/json";

                   var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                   if (contextFeature != null)
                   {
                       await context.Response.WriteAsync(new ErrorDetails()
                       {
                           StatusCode = context.Response.StatusCode,
                           Message = "Internal Server Error."
                       }.ToString());
                   }
               });
           });
        }

        /// <summary>
        /// Static method to inject GlobalException handler into startup Pipeline
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}