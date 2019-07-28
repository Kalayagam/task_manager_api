using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Business.Interfaces;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;
using TaskStatus = TaskManager.Repository.Context.TaskStatus;

namespace TaskManager.Business.Implementations
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectBusiness(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public async Task Add(ProjectViewModel model)
        {
            if (model == null)
            {
                throw new ProjectException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            var projectEntity = new Project()
            {
                ProjectName = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Priority = model.Priority
            };

            await _projectRepository.Add(projectEntity);
        }

        public async Task Delete(int id)
        {
            var projectEntity = await _projectRepository.Get(id);
            if (projectEntity == null)
            {
                throw new TaskDetailsException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            await _projectRepository.Delete(projectEntity);
        }

        public async Task<ProjectViewModel> Get(int id)
        {
            var projectEntity = await _projectRepository.Get(id);

            if (projectEntity == null)
            {
                throw new ProjectException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            var projectViewModel = new ProjectViewModel()
            {
                Id = projectEntity.Id,
                Name = projectEntity.ProjectName,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
                Priority = projectEntity.Priority
            };

            return projectViewModel;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAll()
        {
            var projectEntities = await _projectRepository.GetAll();
            var projects = new List<ProjectViewModel>();
            foreach (var projectEntity in projectEntities)
            {
                var project = new ProjectViewModel()
                {
                    Id = projectEntity.Id,
                    Name = projectEntity.ProjectName,
                    StartDate = projectEntity.StartDate,
                    EndDate = projectEntity.EndDate,
                    Priority = projectEntity.Priority
                };
                if(projectEntity.Tasks != null)
                {
                    project.TotalNumberOfTasks = projectEntity.Tasks.Count();
                    project.NumberOfTasksCompleted = projectEntity.Tasks.Count(x => x.Status == TaskStatus.Complete);
                }
                projects.Add(project);
            }

            return projects;
        }

        public async Task Update(ProjectViewModel model)
        {
            if (model == null)
            {
                throw new ProjectException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            var projectEntity = new Project()
            {
                ProjectName = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Priority = model.Priority

            };
            var projectToBeUpdated = await _projectRepository.Get(model.Id);
            if (projectToBeUpdated != null)
            {
                await _projectRepository.Update(projectToBeUpdated, projectEntity);
            }
        }
    }
}
