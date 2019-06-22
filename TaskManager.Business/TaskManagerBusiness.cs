using System;
using System.Collections;
using System.Threading.Tasks;
using TaskManager.Repository;
using TaskManager.Repository.Context;
using TaskManager.Core;
using System.Collections.Generic;
using TaskManager.Core.Exceptions;
using System.Linq;

namespace TaskManager.Business
{
    public class TaskManagerBusiness : ITaskManagerBusiness
    {
        private readonly ITaskManagerRepository<TaskDetails> _taskManagerRepository;
        public TaskManagerBusiness(ITaskManagerRepository<TaskDetails> taskManagerRepository)
        {
            _taskManagerRepository = taskManagerRepository;
        }

        public async Task<IEnumerable<TaskViewModel>> GetAllTasks()
        {
            var taskEntities = await _taskManagerRepository.GetAll();
            var tasks = new List<TaskViewModel>();
            foreach(var taskEntity in taskEntities)
            {
                tasks.Add(new TaskViewModel()
                {
                    Id = taskEntity.Id,
                    TaskName = taskEntity.TaskName,
                    ParentId = taskEntity.ParentId,
                    ParentTaskName = GetParentTaskName(taskEntity.ParentId, taskEntities) ?? string.Empty,
                    Priority = taskEntity.Priority,
                    StartDate = taskEntity.StartDate,
                    EndDate = taskEntity.EndDate
                });
            }
            
            return await Task.FromResult(tasks);
        }

        public async Task<TaskViewModel> GetTask(int id)
        {
            var taskEntity = await _taskManagerRepository.Get(id);

            if (taskEntity == null)
            {
                throw new TaskDetailsException(ErrorCodes.TaskNotFoundResponse, "Task is empty");
            }

            var taskViewModel = new TaskViewModel()
            {
                TaskName = taskEntity.TaskName,
                ParentId = taskEntity.ParentId,
                Priority = taskEntity.Priority,
                StartDate = taskEntity.StartDate,
                EndDate = taskEntity.EndDate,
            };

            return taskViewModel;
        }


        public async Task AddTask(TaskViewModel taskDetails)
        {
            if (taskDetails == null)
            {
                throw new TaskDetailsException(ErrorCodes.TaskBadRequestResponse, "Task is empty");
            }

            var taskEntity = new TaskDetails()
            {
                TaskName = taskDetails.TaskName,
                ParentId = taskDetails.ParentId,
                Priority = taskDetails.Priority,
                StartDate = taskDetails.StartDate,
                EndDate = taskDetails.EndDate,

            };

            await _taskManagerRepository.Add(taskEntity);
        }

        public async Task UpdateTask(TaskViewModel taskDetails)
        {
            if (taskDetails == null)
            {
                throw new TaskDetailsException(ErrorCodes.TaskNotFoundResponse, "Task is empty");
            }

            var taskEntity = new TaskDetails()
            {
                TaskName = taskDetails.TaskName,
                ParentId = taskDetails.ParentId,
                Priority = taskDetails.Priority,
                StartDate = taskDetails.StartDate,
                EndDate = taskDetails.EndDate,

            };
            var taskToBeUpdated = await _taskManagerRepository.Get(taskDetails.Id);
            if(taskToBeUpdated != null)
            {
                await _taskManagerRepository.Update(taskToBeUpdated, taskEntity);
            }            
        }

        public async Task DeleteTask(int id)
        {
            var taskEntity = await _taskManagerRepository.Get(id);
            if (taskEntity == null)
            {
                throw new TaskDetailsException(ErrorCodes.TaskNotFoundResponse, "Task is empty");
            }

            await _taskManagerRepository.Delete(taskEntity);
        }

        private string GetParentTaskName(int parentId, IEnumerable<TaskDetails> taskEntities)
        {
            return taskEntities.FirstOrDefault(task => task.Id == parentId)?.TaskName;
        }

    }
}
