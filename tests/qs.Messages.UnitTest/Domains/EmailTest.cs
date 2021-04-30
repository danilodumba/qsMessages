using System.Collections.Generic;
using qs.Messages.Domains.Entities;
using qs.Messages.UnitTest.Domains.Mocks;
using qsLibPack.Domain.Exceptions;
using Xunit;

namespace qs.Messages.UnitTest.Domains
{
    public class EmailTest
    {
        [Fact]
        public void Deve_Criar_Email_Valido()
        {
            var email = EmailMock.GetEmail();
            Assert.NotNull(email);
            Assert.True(email.Status == Messages.Domains.Entities.StatusMailEnum.Sending);
        }

        [Fact]
        public void Deve_Enviar_Email()
        {
            var email = EmailMock.GetEmail();
            email.SetStatusSent();
            Assert.True(email.Status == Messages.Domains.Entities.StatusMailEnum.Sent);
        }

        [Fact]
        public void Deve_Informar_Error_Email()
        {
            var email = EmailMock.GetEmail();
            email.SetStatusError("Error message");
            Assert.True(email.Status == Messages.Domains.Entities.StatusMailEnum.Error);
        }

        [Fact]
        public void Deve_Informar_Error_Email_Invalido()
        {
            var email = EmailMock.GetEmail();
            Assert.Throws<DomainException>(() => 
                email.SetStatusError("")
            );
        }

        [Theory]
        [MemberData(nameof(GetInvalidEmails))]
        public void Deve_Criar_Email_Invalido(string to, string from, string subject, Project project)
        {
            Assert.Throws<DomainException>(() => new Email(
                to, 
                from,
                "body",
                project,
                subject
            ));
        }

        public static IEnumerable<object[]> GetInvalidEmails()
        {
            yield return new object[] { "", "danilo@danilo.com.br", "assunto", ProjectMock.GetProject() };
            yield return new object[] { "danilo@danilo.com.br", "", "assunto", ProjectMock.GetProject() };
            yield return new object[] { "danilo@danilo.com.br", "danilo@danilo.com.br", "", ProjectMock.GetProject() };
            yield return new object[] { "danilo@danilo.com.br", "danilo@danilo.com.br", "assunto", null };
        }
    }
}