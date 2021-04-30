using Bogus;
using qs.Messages.Domains.Entities;

namespace qs.Messages.UnitTest.Domains.Mocks
{
    public class EmailMock
    {
        public static Email GetEmail()
        {
            var mock = new Faker<Email>()
            .CustomInstantiator(p => new Email(
                p.Person.Email,
                p.Person.Email,
                p.Lorem.Sentences(10),
                ProjectMock.GetProject(),
                p.Name.JobTitle()
            ));

            return mock.Generate();
        }
    }
}