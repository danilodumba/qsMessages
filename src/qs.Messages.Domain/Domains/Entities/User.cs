using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Validations;

namespace qs.Messages.Domains.Entities
{
    public class User : AggregateRoot<Guid>
    {
        protected User() {}

        public User(string name, string password, string userName, bool admin)
        {
            Name = name;
            Password = password;
            UserName = userName;
            Admin = admin;
            Id = Guid.NewGuid();

            this.Validate();
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string UserName { get; private set; }
        public bool Admin { get; private set; }

        public void SetName(string value)
        {
            this.Name = value;
            this.Validate();
        }

        public void SetUserName(string value)
        {
            this.UserName = value;
            this.Validate();
        }

        public void SetAdmin(bool value)
        {
            this.Admin = value;
        }

        public void SetPassword(string value)
        {
            this.Password = value;
            this.Validate();
        }

        protected override void Validate()
        {
            this.Name.NotNullOrEmpty("Informe um nome");
            this.Password.NotNullOrEmpty("Informe uma senha");
            this.UserName.NotNullOrEmpty("Informe um login");
        }
    }
}