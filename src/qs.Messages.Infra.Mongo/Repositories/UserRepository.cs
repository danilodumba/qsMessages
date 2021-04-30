using System;
using System.Collections.Generic;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Core.Interfaces;

namespace qs.Messages.Infra.Mongo.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }

        public IEnumerable<User> ListByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}