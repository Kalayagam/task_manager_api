using System;
using System.Collections;
using System.Threading.Tasks;
using TaskManager.Repository;
using TaskManager.Repository.Context;
using TaskManager.Core;
using System.Collections.Generic;
using TaskManager.Core.Exceptions;
using System.Linq;
using TaskManager.Repository.Interfaces;
using TaskStatus = TaskManager.Repository.Context.TaskStatus;
using AutoMapper;

namespace TaskManager.Business
{
    public class TaskManagerBusiness : ITaskManagerBusiness
    {
        private readonly ITaskManagerRepository<TaskDetails> _taskManagerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TaskManagerBusiness(ITaskManagerRepository<TaskDetails> taskManagerRepository,
                                    IProjectRepository projectRepository,
                                    IMapper mapper)
        {
            _taskManagerRepository = taskManagerRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskViewModel>> GetAllTasks()
        {
            var taskEntities = await _taskManagerRepository.GetAll();
            var tasks = new List<TaskViewModel>();
            foreach(var taskEntity in taskEntities)
            {
                var taskViewModel = _mapper.Map<TaskViewModel>(taskEntity);
                tasks.Add(taskViewModel);
            }
            
            return tasks;
        }

        public async Task<TaskViewModel> GetTask(int id)
        {
            var taskEntity = await _taskManagerRepository.Get(id);

            if (taskEntity == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskNotFoundResponse, "Task not found");
            }

            var taskViewModel = _mapper.Map<TaskViewModel>(taskEntity);

            return taskViewModel;
        }       

        public async Task AddTask(TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskBadRequestResponse, "Task is empty");
            }

            if(taskViewModel.IsParentTask)
            {
               await AddParentTask(taskViewModel);
            }
            else
            {
                var taskEntity = _mapper.Map<TaskDetails>(taskViewModel);

                taskEntity.ParentTask = await GetParentTask(taskViewModel);
                taskEntity.Project = await GetProject(taskViewModel);

                await _taskManagerRepository.Add(taskEntity);
            }
        }

        private async Task<Project> GetProject(TaskViewModel taskDetails)
        {
            return await _projectRepository.Get(taskDetails.ProjectId);
        }

        public async Task UpdateTask(TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskNotFoundResponse, "Task is empty");
            }

            var taskEntity = _mapper.Map<TaskDetails>(taskViewModel);
            taskEntity.ParentTask = await GetParentTask(taskViewModel);
            taskEntity.Project = await GetProject(taskViewModel);

            var taskToBeUpdated = await _taskManagerRepository.Get(taskViewModel.Id);
            if (taskToBeUpdated == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskNotFoundResponse, "Task not found");
            }

            await _taskManagerRepository.Update(taskToBeUpdated, taskEntity);
        }

        public async Task DeleteTask(int id)
        {
            var taskEntity = await _taskManagerRepository.Get(id);
            if (taskEntity == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskNotFoundResponse, "Task not found");
            }

            await _taskManagerRepository.Delete(taskEntity);
        }

        private async Task AddParentTask(TaskViewModel taskDetails)
        {
            var taskEntity = new ParentTask()
            {
                TaskName = taskDetails.TaskName
            };

            await _taskManagerRepository.Add(taskEntity);
        }

        private async Task<ParentTask> GetParentTask(TaskViewModel taskDetails)
        {
            return taskDetails.ParentId == 0 ? null : await _taskManagerRepository.GetParentTask(taskDetails.ParentId);
        }
    }
}
