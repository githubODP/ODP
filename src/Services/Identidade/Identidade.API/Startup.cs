using Identidade.API.Configuration;
using Identidade.Domain.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Identidade.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);

            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("corregedoria_admin", policy =>
                {
                    policy.RequireClaim("Departamento",
                        ETipoDepartamento.CORREGEDORIA.ToString(),
                        ETipoDepartamento.ADMINISTRADOR.ToString());
                });

                options.AddPolicy("odp_admin", policy =>
                {
                    policy.RequireClaim("Departamento",
                        ETipoDepartamento.ADMINISTRADOR.ToString(),
                        ETipoDepartamento.ODP.ToString());
                });
                options.AddPolicy("gabinete_admin", policy =>
                {
                    policy.RequireClaim("Departamento",
                        ETipoDepartamento.GABINETE.ToString(),
                        ETipoDepartamento.ADMINISTRADOR.ToString());
                });

            });



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
        }
    }
}
