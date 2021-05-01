using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qs.Messages.Domains.Entities;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Domains.Repositories
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        IEnumerable<Project> ListByName(string name);
        Project GetByApiKey(Guid apiKey);
        Task<IEnumerable<Project>> ListAll();
    }
}