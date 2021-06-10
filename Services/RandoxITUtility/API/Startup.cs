using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandoxITUtility.Application;
using RandoxITUtility.Infrastructure.Data.Contexts;
using RandoxITUtilityAPI.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using RandoxITUtility.API.Extensions;

namespace RandoxITUtilityAPI
{
    public class Startup
    {
        private readonly string _MyAllowSpecificOrigins;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _MyAllowSpecificOrigins = Configuration.GetValue<string>("Cors:PolicyName");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureControllerAuthentication();

            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.ConfigureSwaggerGen();
            services.ConfigureCors(Configuration);
            services.AddApplication();

            services.AddDbContext<RandoxITUtilityContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionSettings:DefaultConnection")));

            services.AddHealthChecks().AddDbContextCheck<RandoxITUtilityContext>();

            services.ConfigureDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RandoxITUtilityContext dataContext, TelemetryConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

#if DEBUG
                configuration.DisableTelemetry = true;
#endif

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            // migrate any database changes on startup (includes initial db creation)
            try
            {
                dataContext.Database.Migrate();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"exception caught {e.Message} during SQL Migration");
            }
        }
    }
}
