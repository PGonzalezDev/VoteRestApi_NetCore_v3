using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Configure;

namespace VotesRestApi.Repositories
{
    public abstract class BaseRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public BaseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            var entityEntry = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : EntityBase
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>().AnyAsync(expression);
        }
    }
}
