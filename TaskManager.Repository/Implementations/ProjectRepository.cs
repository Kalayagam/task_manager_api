using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Repository.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskDbContext _taskDbContext;
        public ProjectRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task Add(Project entity)
        {
            _taskDbContext.Projects.Add(entity);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Delete(Project entity)
        {
            _taskDbContext.Projects.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task<Project> Get(int id)
        {           
            return await _taskDbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {            
            return await _taskDbContext.Projects.Include(project => project.Tasks).ToListAsync();
        }        

        public async Task Update(Project entityToBeUpdated, Project entity)
        {
            entityToBeUpdated.ProjectName = entity.ProjectName;
            entityToBeUpdated.StartDate = entity.StartDate;
            entityToBeUpdated.EndDate = entity.EndDate;
            entityToBeUpdated.Priority = entity.Priority;

            await _taskDbContext.SaveChangesAsync();
        }
    }
}
