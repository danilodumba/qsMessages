using System;
using System.Collections.Generic;
using qs.Messages.ApplicationServices.Validations;
using qsLibPack.Mediator;

namespace qs.Messages.ApplicationServices.Command
{
    public class SendMailCommand : Command<Guid>
    {
        public string To { get; set; }
        public Guid ProjectApiKey { get; set; }
        public string TemplateID { get; set; }
        public List<KeyValuePair<string, string>> KeyValues { get; set; } = new List<KeyValuePair<string, string>>();

        public override bool IsValid()
        {
            var validation = new SendMailCommandValidation();
            this.Errors = validation.Validate(this).Errors;

            return this.Errors.Count == 0;
        }
    }
}