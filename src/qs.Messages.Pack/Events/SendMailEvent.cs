using qs.Messages.Pack.Core;
using System;
using System.Collections.Generic;

namespace qs.Messages.Pack.Events
{
    public class SendMailEvent : Message
    {
        public string To { get; set; }
        public Guid ProjectApiKey { get; set; }
        public string TemplateID { get; set; }
        public List<KeyValuePair<string, string>> KeyValues { get; set; } = new List<KeyValuePair<string, string>>();
    }
}   