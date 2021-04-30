using System;
using System.Collections.Generic;
using qs.Messages.Domains.Entities;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Domains.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        IEnumerable<User> ListByName(string name);
    }
}