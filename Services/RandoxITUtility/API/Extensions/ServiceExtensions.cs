using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace RandoxITUtilityAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureControllerAuthentication(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });
            //.AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = $"{configuration["ADAuthentication:Tenant"]}/{configuration["ADAuthentication:Policy"]}/v2.0/";
                o.MetadataAddress = $"{configuration["ADAuthentication:Tenant"]}/v2.0/.well-known/openid-configuration?p={configuration["ADAuthentication:Policy"]}";
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudiences = new List<string>
                    {
                        configuration["ADAuthentication:AppIdUri"],
                        configuration["ADAuthentication:ClientId"],
                        configuration["GraphService:GraphAPIClientID"]
                    }
                };
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                //Used for debugging authentication.
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed,
                    OnChallenge = AuthenticationChallenged,
                    OnTokenValidated = AuthenticationValidated
                };
                o.Validate();
            });
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy($"{configuration["Cors:PolicyName"]}",
                    builder => builder.WithOrigins($"{configuration["Cors:Origin"]}")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            //builder => builder.AllowAnyOrigin()
        }

        /// <summary>
        /// configure OpenAPI documentation for DevSupport
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Randox IT Utility",
                    Description = "API data",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Joshua Prior",
                        Email = "joshua.prior@randox.com"
                    },
                });
            });
        }

        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
        }

        //Used to debug authentication.
        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            arg.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }

        private static Task AuthenticationChallenged(JwtBearerChallengeContext arg)
        {
            return Task.FromResult(0);
        }

        private static Task AuthenticationValidated(TokenValidatedContext arg)
        {
            return Task.FromResult(0);
        }
    }
}