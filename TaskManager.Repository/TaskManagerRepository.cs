using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Repository.Context;

namespace TaskManager.Repository
{
    public class TaskManagerRepository : ITaskManagerRepository<TaskDetails>
    {
        private readonly TaskDbContext _taskDbContext;
        public TaskManagerRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }
       
        public async Task<IEnumerable<TaskDetails>> GetAll()
        {
            return await Task.FromResult(_taskDbContext.TaskDetails.ToList());
        }       

        public async Task<TaskDetails> Get(int id)
        {
            return await Task.FromResult(_taskDbContext.TaskDetails.FirstOrDefault(x => x.Id == id));
        }

        public async Task Add(TaskDetails taskDetails)
        {
            _taskDbContext.TaskDetails.Add(taskDetails);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Update(TaskDetails taskDetailsToBeUpdated, TaskDetails taskDetails)
        {
            taskDetailsToBeUpdated.ParentId = taskDetails.ParentId;
            taskDetailsToBeUpdated.Priority = taskDetails.Priority;
            taskDetailsToBeUpdated.StartDate = taskDetails.StartDate;
            taskDetailsToBeUpdated.TaskName = taskDetails.TaskName;
            taskDetailsToBeUpdated.EndDate = taskDetails.EndDate;

            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Delete(TaskDetails entity)
        {
            _taskDbContext.TaskDetails.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
        }
    }
}
