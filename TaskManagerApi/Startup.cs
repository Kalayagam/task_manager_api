using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Business;
using TaskManager.Business.Implementations;
using TaskManager.Business.Interfaces;
using TaskManager.Repository;
using TaskManager.Repository.Context;
using TaskManager.Repository.Implementations;
using TaskManager.Repository.Interfaces;
using TaskManagerApi.Extensions;
using TaskManagerApi.Middlewares;

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
            services.ConfigureCors();
            services.AddMvc();
            services.ConfigureDBContext(Configuration);
            services.ConfigureSwagger();
            services.ConfigureAutoMapper();
            services.ConfigureDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDevelopmentEnvironmentConfiguration();
            }
            app.UseCors("AllowOrigin");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
