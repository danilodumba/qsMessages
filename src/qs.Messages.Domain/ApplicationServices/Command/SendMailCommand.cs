using System;
using System.Collections.Generic;
using qs.Messages.ApplicationServices.Validations;
using qsLibPack.Mediator;

namespace qs.Messages.ApplicationServices.Command
{
    public class SendMailCommand : Command<Guid>
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public Guid ProjectApiKey { get; set; }
        public string TemplateID { get; set; }
        public List<KeyValueCommand> KeyValues { get; set; } = new List<KeyValueCommand>();

        public override bool IsValid()
        {
            var validation = new SendMailCommandValidation();
            this.Errors = validation.Validate(this).Errors;

            return this.Errors.Count == 0;
        }
    }

    public class KeyValueCommand
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}