using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qs.Messages.Domains.Entities;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Domains.Repositories
{
    public interface ITemplateRepository : IRepository<Template, String>
    {
        IEnumerable<Template> ListByDescription(string description);
        Task<IEnumerable<Template>> ListAll();
    }
}