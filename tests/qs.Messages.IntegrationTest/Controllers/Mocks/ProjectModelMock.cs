using Bogus;
using qs.Messages.ApplicationServices.Models;

namespace qs.Messages.IntegrationTest.Controllers.Mocks
{
    public static class ProjectModelMock
    {
         public static ProjectModel GetProjectModel()
        {
            var mock = new Faker<ProjectModel>()
            .RuleFor(m => m.Name, f => f.Name.JobTitle());
            return mock.Generate();
        }
    }
}