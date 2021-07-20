using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Configure;
using VotesRestApi.Repositories.Interfaces;

namespace VotesRestApi.Repositories
{
    public class VoteRepository : BaseRepository, IVoteRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public VoteRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Vote vote)
        {
            var voteCreated = await base.AddAsync(vote);
            return voteCreated.Id;
        }

        public async Task UpdateAsync(Vote vote)
        {
            await base.UpdateAsync(vote);
        }

        public async Task RemoveAsync(Vote vote)
        {
            await base.RemoveAsync(vote);
        }

        public async Task<bool> AnyAsync(Expression<Func<Vote, bool>> expression)
        {
            return await base.AnyAsync(expression);
        }

        public IEnumerable<VoteDto> GetAllVotes()
        {
            var query = from vote in _dbContext.VoteDbSet
                        join votingUser in _dbContext.UserDbSet on vote.VotingUserId equals votingUser.Id
                        join votedUser in _dbContext.UserDbSet on vote.VotedUserId equals votedUser.Id
                        select new VoteDto()
                        {
                            Id = vote.Id,
                            Date = vote.Date,
                            Comment = vote.Comment,
                            VotingUser = new UserVoteDto() { Id = votingUser.Id, Name = votingUser.Name },
                            VotedUser = new UserVoteDto() { Id = votedUser.Id, Name = votedUser.Name },
                            Nomination = vote.Nomination
                        };

            return query;            
        }

        public VoteDto GetVoteById(Guid id)
        {
            var query = GetAllVotes();

            return query.SingleOrDefault(x => x.Id == id);
        }
    }
}
