using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qs.Messages.Domains.Entities;
using qsLibPack.Domain.ValueObjects.Br;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Domains.Repositories
{
    public interface IEmailRepository : IRepository<Email, Guid>
    {
        Task<IEnumerable<Email>> Serach(PeriodoVO period, Guid? projecID, StatusMailEnum? status);
    }
}