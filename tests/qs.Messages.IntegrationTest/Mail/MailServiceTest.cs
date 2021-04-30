using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qs.Messages.Mail;
using Xunit;

namespace qs.Messages.IntegrationTest.SendingBlue
{
    public class MailServiceTest
    {
        // [Fact]
        // public async Task Deve_Enviar_Email_SMTP()
        // {
        //     var options = Options.Create<MailSettings>(CriarSettings());
        //     var service = new MailService(options);

        //     var email = new qs.Messages.Domains.Entities.Email(
        //         to: "danilo.dumba@quadsys.com.br",
        //         from: "no-reply@easypoup.com",
        //         body: "teste de sistema sending blue",
        //         new Domains.Entities.Project("teste"),
        //         "teste de sistema"
        //     );

        //     await service.Send(email);
        // }

        private MailSettings CriarSettings()
        {
            return new MailSettings
            {
                Port = 587,
                User = "dsdumba@gmail.com",
                Password = "gzDVt5kTyOsZFY9a",
                //Password = "Dumbas@1608",
                //Smtp = "smtp.gmail.com"
                Smtp = "smtp-relay.sendinblue.com"
            };
        }
    }
}