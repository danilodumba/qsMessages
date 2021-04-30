using Bogus;
using qs.Messages.Domains.Entities;

namespace qs.Messages.UnitTest.Domains.Mocks
{
    public static class TemplateMock
    {
        public static Template GetTemplate()
        {
            var mock = new Faker<Template>()
            .CustomInstantiator(p => new Template(
                p.Random.String(20),
                p.Random.String(200),
                p.Lorem.Text(),
                ProjectMock.GetProject()
            ));

            return mock.Generate();
        }
    }
}