using System.Collections.Generic;
using System.Threading.Tasks;
using qsLibPack.Domain.ValueObjects;

namespace qs.Messages.Pack.Services.Interfaces
{
    public interface ISendMailService
    {
        Task Send(EmailVO to, string templateID, params KeyValuePair<string, string>[] values);
    }
}