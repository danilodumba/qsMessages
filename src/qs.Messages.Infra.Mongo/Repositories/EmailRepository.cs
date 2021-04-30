using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Infra.Mongo.Core;
using qs.Messages.Infra.Mongo.Core.Interfaces;
using qsLibPack.Domain.ValueObjects.Br;

namespace qs.Messages.Infra.Mongo.Repositories
{
    public class EmailRepository : Repository<Email, Guid>, IEmailRepository
    {
        public EmailRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Email>> Serach(PeriodoVO period, Guid? projecID, StatusMailEnum? status)
        {
            var emails = _dbSet.Find(e => e.Date >= period.DataInicial && e.Date <= period.DataFinal);
            return await emails.ToListAsync();
        }
    }
}