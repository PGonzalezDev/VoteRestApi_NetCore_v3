using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Core.Models;

namespace VotesRestApi.Repositories.Interfaces
{
    public interface IVoteRepository
    {
        Task<Guid> AddAsync(Vote vote);
        Task UpdateAsync(Vote vote);
        Task RemoveAsync(Vote vote);
        Task<bool> AnyAsync(Expression<Func<Vote, bool>> expression);
        IEnumerable<VoteDto> GetAllVotes();
        VoteDto GetVoteById(Guid id);
    }
}
