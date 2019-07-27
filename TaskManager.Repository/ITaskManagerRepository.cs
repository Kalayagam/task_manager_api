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

        Task<ParentTask> GetParentTask(int id);

        Task Add(TEntity entity);

        Task Add(ParentTask parentTask);

        Task Update(TEntity entityToBeUpdated, TEntity entity);

        Task Delete(TEntity entity);
    }
}
