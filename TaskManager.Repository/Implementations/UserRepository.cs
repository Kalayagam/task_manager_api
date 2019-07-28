using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Repository.Implementations
{
    public class UserRepository: IRepository<User>
    {
        private readonly TaskDbContext _taskDbContext;
        public UserRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task Add(User entity)
        {
            _taskDbContext.Users.Add(entity);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _taskDbContext.Users.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _taskDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _taskDbContext.Users.ToListAsync();
        }

        public async Task Update(User entityToBeUpdated, User entity)
        {
            entityToBeUpdated.FirstName = entity.FirstName;
            entityToBeUpdated.LastName = entity.LastName;
            entityToBeUpdated.EmployeeId = entity.EmployeeId;

            await _taskDbContext.SaveChangesAsync();
        }
    }
}
