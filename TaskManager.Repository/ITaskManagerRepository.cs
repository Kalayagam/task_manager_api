using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Repository.Context;

namespace TaskManager.Repository
{
    public interface ITaskManagerRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(int id);

        Task Add(TEntity entity);

        Task Update(TEntity entityToBeUpdated, TEntity entity);

        Task Delete(TaskDetails entity);

    }
}
