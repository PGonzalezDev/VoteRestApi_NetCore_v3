﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;

namespace VotesRestApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);
        Task<User> GetByIdAsync(Guid id);
        IEnumerable<User> GetAll();
        Task<bool> AnyAsync(Expression<Func<User, bool>> expression);
    }
}
