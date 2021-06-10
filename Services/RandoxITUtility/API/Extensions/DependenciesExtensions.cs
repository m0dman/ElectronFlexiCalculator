using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RandoxITUtility.API.Business.Interfaces;
using RandoxITUtility.API.Business;

namespace RandoxITUtility.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DependenciesExtensions
    {
        /// <summary>
        /// Handle the management for API Dependency Injection
        /// </summary>
        /// <param name="services">startup service collection</param>
        /// <returns>services returned with injected dependencies</returns>
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            Console.WriteLine("ConfigureDependencies");
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ITimeManagementManager, TimeManagementManager>();
        }
    }
}