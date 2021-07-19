using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Configure;
using VotesRestApi.Repositories.Interfaces;

namespace VotesRestApi.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Guid> AddAsync(User user)
        {
            var userCreated = await base.AddAsync(user);
            return userCreated.Id;
        }

        public async Task UpdateAsync(User user)
        {
            await base.UpdateAsync(user);
        }

        public async Task RemoveAsync(User user)
        {
            await base.RemoveAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync<User>(id);
        }

        public IEnumerable<User> GetAll()
        {
            return base.GetAll<User>();
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await base.AnyAsync(expression);
        }
    }
}
