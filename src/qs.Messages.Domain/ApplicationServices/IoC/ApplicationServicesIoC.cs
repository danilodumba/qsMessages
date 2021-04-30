using Microsoft.Extensions.DependencyInjection;
using qs.Messages.ApplicationServices.Services;
using qs.Messages.ApplicationServices.Services.Interfaces;

namespace qs.Messages.ApplicationServices.IoC
{
    public static class ApplicationServicesIoC
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}