using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using qs.Messages.ApplicationServices.IoC;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Mail.IoC;
using qsLibPack.Validations.IoC;
using qs.Messages.Infra.Mongo.IoC;
using qsLibPack.Middlewares;
using System;
using qs.Messages.Pack;
using Rebus.ServiceProvider;
using qs.Messages.Pack.Events;

namespace qs.Messages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddQsMessage(Configuration);
            services.AddRepositoryMongo(Configuration);
            services.AddMail(Configuration);
            services.AddApplicationServices();
            var assembly = AppDomain.CurrentDomain.Load("qs.Messages.Domain");
            services.AddMediatR(assembly);
            services.AddValidationService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "qs.Messages", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "qs.Messages v1"));
            }

            // app.UseHttpsRedirection();

            app.ApplicationServices.UseRebus(c =>
            {
                c.Subscribe<SendMailEvent>().Wait();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseValidationService(); //Usando a validacao de mensagens do qsLibPack

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
