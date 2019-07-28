﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TaskManager.Business;
using TaskManager.Business.AutoMapper;
using TaskManager.Business.Implementations;
using TaskManager.Business.Interfaces;
using TaskManager.Repository;
using TaskManager.Repository.Context;
using TaskManager.Repository.Implementations;
using TaskManager.Repository.Interfaces;

namespace TaskManagerApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddMvc();            

            services.AddDbContext<TaskDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("TaskManager")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Task Manager API", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<ITaskManagerBusiness, TaskManagerBusiness>();
            services.AddTransient<IProjectBusiness, ProjectBusiness>();

            services.AddTransient<ITaskManagerRepository<TaskDetails>, TaskManagerRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                  {
                      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager V1");
                  });
            }
            app.UseCors("AllowOrigin");

            app.UseMvc();
        }
    }
}
