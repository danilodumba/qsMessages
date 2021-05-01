using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.ServiceProvider;

namespace qs.Messages.Pack
{
    public static class ServiceCollectionExtensions
    {
         public static void AddQsMessage(this IServiceCollection services, IConfiguration configuration)
         {
            var settings = configuration.GetSection("MessageSettings");
            var queue = settings.GetValue<string>("Queue");
            var connectionRabbit = settings.GetValue<string>("RabbitConnection");

            services.AddRebus(c => c
                .Transport(t => t.UseRabbitMq(connectionRabbit, queue))
            );

            services.Configure<MessageSettings>(configuration.GetSection("MessageSettings"));
         }
    }
}