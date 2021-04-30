using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace qs.Messages.Infra.Mongo.Core.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}