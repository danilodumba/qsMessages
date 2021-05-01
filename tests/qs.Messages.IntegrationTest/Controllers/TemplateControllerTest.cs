using System;
using System.Threading.Tasks;
using qs.Messages.IntegrationTest.Controllers.Mocks;
using qs.Messages.IntegrationTest.Core;
using Xunit;

namespace qs.Messages.IntegrationTest.Controllers
{
    public class TemplateControllerTest : HostBase
    {
        public TemplateControllerTest() : base("/template")
        {
        }

        [Fact]
        public async Task Deve_Criar_Template()
        {
            var model = TemplateModelMock.GetTemplate();
            model.ProjectID = await CriarProjeto();
            
            var response = await this.Post(model);

            Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<Guid> CriarProjeto()
        {
            var controller = new ProjectControllerTest();
            return await controller.CriarProjeto();
        }
    }
}