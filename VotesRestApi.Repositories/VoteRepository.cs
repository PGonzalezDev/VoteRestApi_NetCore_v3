using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async Task<Vote> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync<Vote>(id);
        }

        public IEnumerable<Vote> GetAll()
        {
            return base.GetAll<Vote>();
        }

        public async Task<bool> AnyAsync(Expression<Func<Vote, bool>> expression)
        {
            return await base.AnyAsync(expression);
        }

        public async Task<IEnumerable<dynamic>> GetVotes(Guid? id)
        {
            var query = from vote in _dbContext.VoteDbSet
                        join votingUser in _dbContext.UserDbSet on vote.VotingUserId equals votingUser.Id
                        join votedUser in _dbContext.UserDbSet on vote.VotedUserId equals votedUser.Id
                        select new
                        {
                            vote.Id,
                            vote.Date,
                            vote.Comment,
                            //new { votingUser.Id, votingUser.Name } as VotingUser,
                        };

            return query;

            
        }
    }
}
