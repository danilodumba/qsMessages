using System.Threading.Tasks;
using qs.Messages.Domains.Entities;

namespace qs.Messages.Domains.Services
{
    public interface IEmailService
    {
        Task Send(Email email);
    }
}