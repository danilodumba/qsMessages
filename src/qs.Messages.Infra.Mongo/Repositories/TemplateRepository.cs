using System;
using System.Collections.Generic;
using MongoDB.Driver;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Core.Interfaces;

namespace qs.Messages.Infra.Mongo.Repositories
{
    public class TemplateRepository : Repository<Template, String>, ITemplateRepository
    {
        public TemplateRepository(IMongoContext context) : base(context)
        {
        }

        public IEnumerable<Template> ListByDescription(string description)
        {
            var all = _dbSet.Find(Builders<Template>.Filter.Empty);
            return all.ToList();
        }
    }
}