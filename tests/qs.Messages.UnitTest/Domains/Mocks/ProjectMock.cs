using Bogus;
using qs.Messages.Domains.Entities;

namespace qs.Messages.UnitTest.Domains.Mocks
{
    public static class ProjectMock
    {
        public static Project GetProject()
        {
            var mock = new Faker<Project>()
            .CustomInstantiator(p => new Project(
                p.Name.JobDescriptor()
            ));

            return mock.Generate();
        }
    }
}