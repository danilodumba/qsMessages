using System.Collections.Generic;
using qs.Messages.Domains.Entities;
using qs.Messages.UnitTest.Domains.Mocks;
using qsLibPack.Domain.Exceptions;
using Xunit;

namespace qs.Messages.UnitTest.Domains
{
    public class TemplateTest
    {
        [Fact]
        public void Deve_Criar_Template_Valido()
        {
            var template = TemplateMock.GetTemplate();
            Assert.NotNull(template);   
        }

        [Theory]
        [MemberData(nameof(GetTemplates))]
        public void Deve_Validar_Template(string id, string mailTemplate, Project project, string mailFrom, string subject)
        {
            Assert.Throws<DomainException>(() => new Template(
                id, 
                "Teste",
                mailTemplate,
                project,
                subject,
                mailFrom
            ));
        }

        public static IEnumerable<object[]> GetTemplates()
        {
            yield return new object[] { "", "Teste de Mail Template", ProjectMock.GetProject(), "teste@teste.com.br", "assunto" };
            yield return new object[] { "TEMPLATE_ID", "", ProjectMock.GetProject(), "teste@teste.com.br", "assunto" };
            yield return new object[] { "TEMPLATE_ID", "Teste de Mail Template", null, "teste@teste.com.br", "assunto" };
            yield return new object[] { "TEMPLATE_ID", "Teste de Mail Template", ProjectMock.GetProject(), "", "assunto" };
            yield return new object[] { "TEMPLATE_ID", "Teste de Mail Template", ProjectMock.GetProject(), "teste@teste.com.br", "" };
        }
    }
}