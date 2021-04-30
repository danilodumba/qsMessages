using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Domain.ValueObjects;
using qsLibPack.Validations;

namespace qs.Messages.Domains.Entities
{
    public class Email : AggregateRoot<Guid>
    {
        public Email() {}
        public Email(EmailVO to, EmailVO from, string body, Project project, string subject)
        {
            To = to.ToString();
            From = from.ToString();
            Body = body;
            Project = project;
            Id = Guid.NewGuid();
            Status = StatusMailEnum.Sending;
            Date = DateTime.Now;
            Subject = subject;

            this.Validate();
        }

        public string To { get; private set; }
        public string From { get; private set; }
        public string Body { get; private set; }
        public string Subject { get; private set; }
        public string ErrorDescription { get; private set; }
        public DateTime Date { get; private set; }
        public StatusMailEnum Status { get; private set; }
        public virtual Project Project { get; private set; }

        public void SetStatusSent()
        {
            this.Status = StatusMailEnum.Sent;
        }

        public void SetStatusError(string description)
        {
            this.Status = StatusMailEnum.Error;
            this.ErrorDescription = description;
            this.ErrorDescription.NotNullOrEmpty("Informe uma descricao para o erro.");
        }

        protected override void Validate()
        {
            this.To.NotNullOrEmpty("Informe um e-mail de envio.");
            this.From.NotNullOrEmpty("Informe um e-mail do remetente.");
            this.Id.NotNull("Informe um ID");
            this.Subject.NotNullOrEmpty("Infome um assunto.");
            this.Project.NotNull("Infome um projeto.");
        }
    }
}