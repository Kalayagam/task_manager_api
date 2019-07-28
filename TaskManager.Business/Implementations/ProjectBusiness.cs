﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Business.Interfaces;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Business.Implementations
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectBusiness(IProjectRepository projectRepository,
                                IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }


        public async Task Add(ProjectViewModel model)
        {
            if (model == null)
            {
                throw new ProjectException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            var projectEntity = _mapper.Map<Project>(model);
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

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectEntity);

            return projectViewModel;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAll()
        {
            var projectEntities = await _projectRepository.GetAll();
            var projects = new List<ProjectViewModel>();
            foreach (var projectEntity in projectEntities)
            {
                var projectViewModel = _mapper.Map<ProjectViewModel>(projectEntity);                
                projects.Add(projectViewModel);
            }

            return projects;
        }

        public async Task Update(ProjectViewModel model)
        {
            if (model == null)
            {
                throw new ProjectException(ErrorCodes.ProjectNotFoundResponse, "Project is empty");
            }

            var projectEntity = _mapper.Map<Project>(model);
            var projectToBeUpdated = await _projectRepository.Get(model.Id);
            if (projectToBeUpdated != null)
            {
                await _projectRepository.Update(projectToBeUpdated, projectEntity);
            }
        }
    }
}