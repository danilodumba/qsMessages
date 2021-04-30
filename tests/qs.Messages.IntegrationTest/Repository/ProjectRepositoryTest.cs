using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Contexts;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Repositories;
using Xunit;

namespace qs.Messages.IntegrationTest.Repository
{
    public class ProjectRepositoryTest
    {
        readonly IOptions<MongoSettings> options;
        readonly MongoContext context; 
        readonly IProjectRepository repository;

        public ProjectRepositoryTest()
        {
            options = Options.Create<MongoSettings>(CriarSettings());
            context = new MongoContext(options);
            repository = new ProjectRepository(context);
        }

        // [Fact]
        // public async Task<Guid> Deve_Criar_Projeto()
        // {
        //     var project = new Project("Teste de Projeto");
        //     await repository.CreateAsync(project);
        //     await context.SaveChangesAsync();

        //     return project.Id;
        // }

        // [Fact]
        // public async Task Deve_Retornar_Projeto()
        // {
        //     var id = await this.Deve_Criar_Projeto();
        //     var p = await repository.GetByIDAsync(id);

        //     Assert.Equal(p.Id, id);
        // }

        // [Fact]
        // public async Task Deve_Alterar_Projeto()
        // {
        //     var id = await this.Deve_Criar_Projeto();
        //     var p = await repository.GetByIDAsync(id);
        //     p.SetName("Nome alterado");
        //     repository.Update(p);
        //     await context.SaveChangesAsync();
        // }

        private MongoSettings CriarSettings()
        {
            return new MongoSettings
            {
                Database = "messageDB",
                ConnectionString = "mongodb://localhost:27017"
            };
        }
    }
}