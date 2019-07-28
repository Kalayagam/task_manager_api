using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskStatus = TaskManager.Repository.Context.TaskStatus;


namespace TaskManager.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureTask();
            ConfigureProject();
        }

        private void ConfigureProject()
        {
            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.TotalNumberOfTasks, opt => opt.MapFrom(src => src.Tasks.Count()))
                .ForMember(dest => dest.NumberOfTasksCompleted, opt => opt.MapFrom(src => src.Tasks.Count(x => x.Status == TaskStatus.Complete)));

            CreateMap<ProjectViewModel, Project>();
        }

        private void ConfigureTask()
        {
            CreateMap<TaskDetails, TaskViewModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentTask != null ? src.ParentTask.Id : 0))
                .ForMember(dest => dest.ParentTaskName, opt => opt.MapFrom(src => src.ParentTask != null ? src.ParentTask.TaskName: string.Empty));

            CreateMap<TaskViewModel, TaskDetails>()
                .ForMember(dest => dest.ParentTask, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());
        }
    }
}
