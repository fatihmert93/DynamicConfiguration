using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicConfig.Lib.DataAccess
{
    public interface IRepository<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetOne(string id);
        Task Create(TModel model);
        Task<bool> Delete(string id);
        Task<long> GetNextId();
    }
}