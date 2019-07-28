using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskStatus = TaskManager.Repository.Context.TaskStatus;


namespace TaskManager.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.TotalNumberOfTasks, opt => opt.MapFrom(src => src.Tasks.Count()))
                .ForMember(dest => dest.NumberOfTasksCompleted, opt => opt.MapFrom(src => src.Tasks.Count(x => x.Status == TaskStatus.Complete)));

            CreateMap<ProjectViewModel, Project>();
        }
    }
}
