using System;

namespace qs.Messages.ApplicationServices.Models
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ApiKey { get; set; }
    }
}