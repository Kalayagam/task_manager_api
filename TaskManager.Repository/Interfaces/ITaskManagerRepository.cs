using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Repository.Interfaces
{
    public interface ITaskManagerRepository<TEntity> : IRepository<TEntity>
    {
        Task<ParentTask> GetParentTask(int id);       

        Task Add(ParentTask parentTask);
    }
}
