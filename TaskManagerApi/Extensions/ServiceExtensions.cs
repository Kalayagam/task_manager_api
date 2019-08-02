using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TaskManager.Business.AutoMapper;
using TaskManager.Business.Implementations;
using TaskManager.Business.Interfaces;
using TaskManager.Core;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskManager.Repository.Implementations;
using TaskManager.Repository.Interfaces;

namespace TaskManagerApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("TaskManager")));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Task Manager API", Version = "v1" });
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            AddBusinessLayerDependencies(services);
            AddRepositoryLayerDependencies(services);
        }

        private static void AddRepositoryLayerDependencies(IServiceCollection services)
        {
            services.AddTransient<ITaskManagerRepository<TaskDetails>, TaskManagerRepository>();
            services.AddTransient<IRepository<Project>, ProjectRepository>(); 
            services.AddTransient<IRepository<User>, UserRepository>();
        }

        private static void AddBusinessLayerDependencies(IServiceCollection services)
        {
            services.AddTransient<ITaskBusiness, TaskManagerBusiness>();
            services.AddTransient<IBusiness<ProjectViewModel>, ProjectBusiness>();
            services.AddTransient<IBusiness<UserViewModel>, UserBusiness>();
        }
    }
}
