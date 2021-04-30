using System.Threading.Tasks;
using qs.Messages.Infra.Mongo.Core.Interfaces;
using qsLibPack.Repositories.Interfaces;

namespace qs.Messages.Infra.Mongo.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IMongoContext _context;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}