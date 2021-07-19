using System;
using System.Collections.Generic;
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

        public async Task AddAsync(User user)
        {
            await base.AddAsync(user);
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
    }
}
