using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Interfaces
{
    public interface IBusiness<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> Get(int id);

        Task Add(TModel model);

        Task Update(TModel model);

        Task Delete(int id);
    }
}
