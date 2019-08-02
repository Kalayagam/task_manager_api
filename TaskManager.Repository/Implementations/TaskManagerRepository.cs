using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Repository.Implementations
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
            return await _taskDbContext.TaskDetails
                .Include(task => task.Project)
                .Include(task => task.ParentTask)
                .Include(task => task.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParentTask>> GetAllParentTasks()
        {
            return await _taskDbContext.ParentTasks.ToListAsync();
        }

        public async Task<TaskDetails> Get(int id)
        {
            return await _taskDbContext.TaskDetails
                .Include(task => task.Project)
                .Include(task => task.ParentTask)
                .Include(task => task.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ParentTask> GetParentTask(int id)
        {
            return await _taskDbContext.ParentTasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(TaskDetails taskDetails)
        {
            _taskDbContext.TaskDetails.Add(taskDetails);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Add(ParentTask parentTask)
        {
            _taskDbContext.ParentTasks.Add(parentTask);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Update(TaskDetails taskDetailsToBeUpdated, TaskDetails taskDetails)
        {
            taskDetailsToBeUpdated.ParentTask = taskDetails.ParentTask;
            taskDetailsToBeUpdated.Project = taskDetails.Project;
            taskDetailsToBeUpdated.User = taskDetails.User;
            taskDetailsToBeUpdated.Priority = taskDetails.Priority;
            taskDetailsToBeUpdated.StartDate = taskDetails.StartDate;
            taskDetailsToBeUpdated.TaskName = taskDetails.TaskName;
            taskDetailsToBeUpdated.EndDate = taskDetails.EndDate;
            taskDetailsToBeUpdated.Status = taskDetails.Status;

            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Delete(TaskDetails entity)
        {
            _taskDbContext.TaskDetails.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
        }      
    }
}
