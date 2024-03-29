﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Business.Interfaces;
using TaskManager.Core;
using TaskManager.Core.Exceptions;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Business.Implementations
{
    public class TaskManagerBusiness : ITaskBusiness
    {
        private readonly ITaskManagerRepository<TaskDetails> _taskManagerRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public TaskManagerBusiness(ITaskManagerRepository<TaskDetails> taskManagerRepository,
                                    IRepository<Project> projectRepository,
                                    IRepository<User> userRepository,
                                    IMapper mapper)
        {
            _taskManagerRepository = taskManagerRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskViewModel>> GetAll()
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

        public async Task<IEnumerable<TaskViewModel>> GetAllParent()
        {
            var parentTaskEntities = await _taskManagerRepository.GetAllParentTasks();
            var parentTasks = new List<TaskViewModel>();
            foreach (var parentTaskEntity in parentTaskEntities)
            {
                var taskViewModel = _mapper.Map<TaskViewModel>(parentTaskEntity);
                parentTasks.Add(taskViewModel);
            }

            return parentTasks;
        }

        public async Task<TaskViewModel> Get(int id)
        {
            var taskEntity = await GetTaskDetails(id);
            var taskViewModel = _mapper.Map<TaskViewModel>(taskEntity);

            return taskViewModel;
        }

        public async Task Add(TaskViewModel taskViewModel)
        { 
            if(taskViewModel.IsParentTask)
            {
               await AddParentTask(taskViewModel);
            }
            else
            {
                var taskEntity = _mapper.Map<TaskDetails>(taskViewModel);

                taskEntity.ParentTask = await GetParentTask(taskViewModel);
                taskEntity.Project = await GetProject(taskViewModel);
                taskEntity.User = await GetUser(taskViewModel);

                await _taskManagerRepository.Add(taskEntity);
            }
        }

        public async Task Update(TaskViewModel taskViewModel)
        {
            var taskToBeUpdated = await GetTaskDetails(taskViewModel.Id);
            var taskEntity = _mapper.Map<TaskDetails>(taskViewModel);
            taskEntity.ParentTask = await GetParentTask(taskViewModel);
            taskEntity.Project = await GetProject(taskViewModel);  
            taskEntity.User = await GetUser(taskViewModel);

            await _taskManagerRepository.Update(taskToBeUpdated, taskEntity);
        }
       
        public async Task Delete(int id)
        {
            var taskEntity = await GetTaskDetails(id);
            await _taskManagerRepository.Delete(taskEntity);
        }

        private async Task<TaskDetails> GetTaskDetails(int id)
        {
            var taskEntity = await _taskManagerRepository.Get(id);

            if (taskEntity == null)
            {
                throw new TaskManagerException(ErrorCodes.TaskNotFoundResponse, "Task not found");
            }

            return taskEntity;
        }

        private async Task<Project> GetProject(TaskViewModel taskDetails)
        {
            return await _projectRepository.Get(taskDetails.ProjectId);
        }

        private async Task<User> GetUser(TaskViewModel taskViewModel)
        {
            return await _userRepository.Get(taskViewModel.UserId);
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
