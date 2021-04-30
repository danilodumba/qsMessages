using qs.Messages.Domains.Entities;
using qs.Messages.UnitTest.Domains.Mocks;
using qsLibPack.Domain.Exceptions;
using Xunit;

namespace qs.Messages.UnitTest.Domains
{
    public class ProjectTest
    {
        [Fact]
        public void Deve_Criar_Projeto_Valido()
        {
            var project = ProjectMock.GetProject();

            Assert.NotNull(project);
        }

        [Fact]
        public void Deve_Criar_Projeto_Invalido()
        {
            Assert.Throws<DomainException>(() => new Project(""));
        }
    }
}