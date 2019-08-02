using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDevelopmentEnvironmentConfiguration(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager V1");
            });
        }
    }
}
