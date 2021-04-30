using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Contexts;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Core.Interfaces;
using qs.Messages.Infra.Mongo.Repositories;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Infra.Mongo.IoC
{
    public static class RepositoryMongoIoC
    {
        public static void AddRepositoryMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("MongoConnection"));


            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
        }
    }
}