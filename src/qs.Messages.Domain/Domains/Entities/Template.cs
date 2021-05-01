using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Validations;

namespace qs.Messages.Domains.Entities
{
    public class Template : AggregateRoot<String>
    {
        public Template(string id, string description, string mailTemplate, Project project, string subject, string mailFrom)
        {
            Description = description;
            MailTemplate = mailTemplate;
            Id = id;
            Subject = subject;
            MailFrom = mailFrom;

            this.SetProject(project);
            this.Validate();
        }

        public string Description { get; private set; }
        public string MailTemplate { get; private set; }
        public Guid ProjectID { get; private set; }
        public string Subject { get; private set; }
        public string MailFrom { get; private set; }

        public void SetProject(Project value)
        {
            value.NotNull("Informe um projeto para o template");
            this.ProjectID = value.Id;
        }

        public void SetDescription(string value)
        {
            this.Description = value;
        }

        public void SetMailTemplate(string value)
        {
            this.MailTemplate = value;
            Validate();
        }

        public void SetMailFrom(string value)
        {
            this.MailFrom = value;
            Validate();
        }

        public void SetSubject(string value)
        {
            this.Subject = value;
            Validate();
        }

        protected override void Validate()
        {
            this.Id.NotNullOrEmpty("Informe um ID para o template.");
            this.MailTemplate.NotNullOrEmpty("Informe um template de e-mail");
            this.ProjectID.NotNull("Informe um projeto para o template.");
            this.MailFrom.NotNullOrEmpty("Informe um email de para o template.");
            this.Subject.NotNullOrEmpty("Informe um assunto para o template.");
        }
    }
}