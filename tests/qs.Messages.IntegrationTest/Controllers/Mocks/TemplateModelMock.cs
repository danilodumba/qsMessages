using System;
using Bogus;
using qs.Messages.ApplicationServices.Models;

namespace qs.Messages.IntegrationTest.Controllers.Mocks
{
    public static class TemplateModelMock
    {
        public static TemplateModel GetTemplate()
        {
            var mock = new Faker<TemplateModel>()
            .RuleFor(m => m.Description, f => f.Lorem.Text())
            .RuleFor(m => m.Id, f => f.Person.UserName)
            .RuleFor(m => m.MailTemplate, f => f.Lorem.Sentences(20))
            .RuleFor(m => m.ProjectID, Guid.NewGuid());
        
            return mock.Generate();
        }
    }
}