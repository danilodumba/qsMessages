using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Validations;

namespace qs.Messages.Domains.Entities
{
    public class Project : AggregateRoot<Guid>
    {
        public Project(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            this.GenereteApiKey();

            this.Validate();
        }

        public string Name { get; private set; }
        public Guid ApiKey { get; private set; }

        public void SetName(string value)
        {
            this.Name = value;
            this.Validate();
        }

        public void GenereteApiKey()
        {
            ApiKey = Guid.NewGuid();
        }

        protected override void Validate()
        {
            this.Name.NotNullOrEmpty("Informe um nome para o projeto.");
        }
    }
}