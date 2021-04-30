using System;

namespace qs.Messages.ApplicationServices.Models
{
    public class TemplateModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string MailTemplate { get; set; }
        public Guid ProjectID { get; set; }
    }
}   