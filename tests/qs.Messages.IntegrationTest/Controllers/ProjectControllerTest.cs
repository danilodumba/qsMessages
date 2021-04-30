using System;
using System.Threading.Tasks;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.IntegrationTest.Controllers.Mocks;
using qs.Messages.IntegrationTest.Core;
using Xunit;

namespace qs.Messages.IntegrationTest.Controllers
{
    public class ProjectControllerTest: HostBase
    {
        public ProjectControllerTest() : base("/project")
        {
        }

        [Fact]
        public async Task Deve_Criar_Projeto_ComSucesso()
        {
            var model = ProjectModelMock.GetProjectModel();
            var response = await this.Post("", model);

            Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }   


        public async Task<Guid> CriarProjeto()
        {
            var model = ProjectModelMock.GetProjectModel();
            var response = await this.Post("", model);
            return await this.ResultResponse<Guid>(response);
        }

        public async Task<ProjectModel> ObterProjeto()
        {
            var id = await this.CriarProjeto();
            var response = await this.Get("/project/" + id.ToString());
            return await this.ResultResponse<ProjectModel>(response);
        }
    }
}