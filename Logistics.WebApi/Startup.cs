using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Logistics.Infrastructure.Data;
using Logistics.Infrastructure.Settings;
using Logistics.WebApi.Infrastructure.Helpers;
using Logistics.WebApi.Middleware;
using Logistics.WebApi.Utility.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.WebApi
{
    internal class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _hostEnvironment { get; }
        public IAppSettings _settings { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = webHostEnvironment;
            _settings = new AppSettings(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<XhwtDbContext>(options =>
               options.UseNpgsql(_configuration.GetConnectionString("DbContext")));


            services.AddCustomRepositories();
            services.AddCustomServices();

            services.AddFluentValidationAutoValidation();
            services.AddCustomValidators();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            IdentityHelper.ConfigureService(services);

            AuthenticationHelper.ConfigureService(services, _settings.Issuer, _settings.Audience, _settings.Key);

            if (_hostEnvironment.IsDevelopment())
            {
                SwaggerHelper.ConfigureService(services);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseMiddleware<ValidationMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            #region Swagger

            if (_hostEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logictics Api v1");
                });
            }

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}