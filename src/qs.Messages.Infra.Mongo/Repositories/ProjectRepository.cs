using System;
using System.Collections.Generic;
using MongoDB.Driver;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Core.Interfaces;

namespace qs.Messages.Infra.Mongo.Repositories
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {
        public ProjectRepository(IMongoContext context) : base(context)
        {
        }

        public Project GetByApiKey(Guid apiKey)
        {
            var data = _dbSet.Find(Builders<Project>.Filter.Eq("ApiKey", apiKey));
            return data.FirstOrDefault();
        }

        public IEnumerable<Project> ListByName(string name)
        {
            var all = _dbSet.Find(Builders<Project>.Filter.Empty);
            return all.ToList();
        }
    }
}