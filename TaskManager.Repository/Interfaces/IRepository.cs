using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(int id);

        Task Add(TEntity entity);

        Task Update(TEntity entityToBeUpdated, TEntity entity);

        Task Delete(TEntity entity);
    }
}
