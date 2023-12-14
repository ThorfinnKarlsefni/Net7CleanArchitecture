using System.Text.Json;
using FluentValidation.AspNetCore;
using Logistics.Infrastructure.Data;
using Logistics.Infrastructure.Settings;
using Logistics.WebApi.Helpers;
using Logistics.WebApi.Middleware;
using Logistics.WebApi.Utility.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistics.WebApi
{
    internal class Startup
    {
        public IConfiguration _configuration;
        public IWebHostEnvironment _hostEnvironment;
        public IAppSettings _appSettings;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _appSettings = new AppSettings(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<XhwtDbContext>(options =>
               options.UseNpgsql(_configuration.GetConnectionString("DbContext")));

            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddCustomServices();

            services.AddCustomRepositories();

            services.AddFluentValidationAutoValidation();

            services.AddCustomValidators();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).FirstOrDefault();
                    return new BadRequestObjectResult(errors);
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            IdentityHelper.ConfigureService(services);

            AuthenticationHelper.ConfigureService(services, _appSettings.Issuer, _appSettings.Audience, _appSettings.Key);

            SerilogHelper.ConfigureService(services);

            services.AddEndpointsApiExplorer();

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

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            #region Swagger

            if (_hostEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "xhwt.Logistics API");
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

