using System;
using System.Threading.Tasks;
using qs.Messages.ApplicationServices.Command;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.IntegrationTest.Controllers.Mocks;
using qs.Messages.IntegrationTest.Core;
using Xunit;

namespace qs.Messages.IntegrationTest.Controllers
{
    public class EmailControllerTest : HostBase
    {
        ProjectControllerTest projectService = new ProjectControllerTest();

        public EmailControllerTest() : base("/email")
        {
        }

        [Fact]
        public async Task Deve_Enviar_Email_Sem_Template_ComSucesso()
        {
            var model = new SendMailCommand
            {
                To = "danilodumba@yahoo.com.br",
                From = "imob@qsse.com.br",
                Body = "Mais um teste de e-mail dos bons. <h1>teste</h1>",
                Subject = "teste de e-mail sem template",
            };

            var projeto = await this.projectService.ObterProjeto();
            model.ProjectApiKey = projeto.ApiKey;
            
            var response = await this.Post(model);

            Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Deve_Enviar_Email_Com_Template_ComSucesso()
        {
            var model = new SendMailCommand
            {
                To = "danilodumba@yahoo.com.br",
                From = "imob@qsse.com.br",
                Subject = "teste de e-mail sem template",
                TemplateID = await CriarTemplate()
            };

            model.KeyValues.Add(new KeyValueCommand { Key = "{nome}", Value = "danilo dumba" });
            model.KeyValues.Add(new KeyValueCommand { Key = "{email}", Value = "dsdumba@gmail.com" });
            model.KeyValues.Add(new KeyValueCommand { Key = "{teste}", Value = "Coisa boa!" });

            var projeto = await this.projectService.ObterProjeto();
            model.ProjectApiKey = projeto.ApiKey;
            
            var response = await this.Post(model);

            Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<string> CriarTemplate()
        {
            var model = TemplateModelMock.GetTemplate();
            model.MailTemplate = @"Isso eh um teste de e-mail com template
                <p>Nome: {nome}</p>
                <p>Email: {email}</p>
                <p>Teste: {teste}</p>
                ";
                
            model.ProjectID = await this.projectService.CriarProjeto();

            var response = await this.Post("/template", model);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao cria template de e-mail com chaves - " + await response.Content.ReadAsStringAsync());

            return model.Id;
        }
       
    }
}