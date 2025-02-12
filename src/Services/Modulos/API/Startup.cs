using API.Configuration;
using CGEODP.WebApi.Core.Identidade;
using Domain.Notificação.Entidade;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;


namespace API
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


            //// Configuração do SMTP
            //var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            //services.AddSingleton(emailSettings);

            //services.AddFluentEmail(emailSettings.SenderEmail)
            //    .AddRazorRenderer()
            //    .AddSmtpSender(new SmtpClient
            //    {
            //        Host = emailSettings.SmtpHost,
            //        Port = emailSettings.SmtpPort,
            //        EnableSsl = emailSettings.EnableSsl,
            //        Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.SenderPassword)
            //    });
            services.AddApiConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddJwtConfiguration(Configuration);
            services.RegisterServices();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfiguration(env);
            app.UseSwaggerConfiguration();
        }



    }

}
