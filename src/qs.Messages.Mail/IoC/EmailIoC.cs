using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qs.Messages.Domains.Services;

namespace qs.Messages.Mail.IoC
{
    public static class MailIoC
    {
        public static void AddMail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailService, MailService>();
        }
    }
}