using System.Collections.Generic;
using System.Threading.Tasks;

namespace qs.Messages.ApplicationServices.Core
{
    public interface IApplicationService<TModel, TId> where TModel : class
    {
        Task<TId> Create(TModel model);
        void Update(TModel model);
        Task<TModel> GetByID(TId id);
        Task Delete(TId id);
    }
}