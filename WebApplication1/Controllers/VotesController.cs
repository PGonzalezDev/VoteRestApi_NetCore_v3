using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotesRestApi.Core.Models;
using VotesRestApi.Service.Context;

namespace WebApplication1.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly VoteContext _context;
        private readonly UserContext _userContext;

        public VotesController(VoteContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: api/votes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vote>>> GetVoteDbSet()
        {
            await MockVotes();

            return await _context.VoteDbSet.ToListAsync();
        }

        // GET: api/votes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> GetVote(Guid id)
        {
            var vote = await _context.VoteDbSet.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }

        // PUT: api/votes/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVote(Guid id, Vote vote)
        {
            if (id != vote.Id)
            {
                return BadRequest();
            }

            _context.Entry(vote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/votes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Vote>> PostVote(Vote vote)
        {
            vote.Id = Guid.NewGuid();
            vote.Date = DateTime.Now;

            await Validate(vote);

            _context.VoteDbSet.Add(vote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVote", new { id = vote.Id }, vote);
        }

        // DELETE: api/votes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vote>> DeleteVote(Guid id)
        {
            var vote = await _context.VoteDbSet.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            _context.VoteDbSet.Remove(vote);
            await _context.SaveChangesAsync();

            return vote;
        }

        private async Task MockVotes()
        {
            var users = await _userContext.UserDbSet.ToListAsync();

            if (users.Any())
            {
                var ginobili = users.SingleOrDefault(x => x.Mail.CompareTo("manu@ginobili.com") == 0);
                var scola = users.SingleOrDefault(x => x.Mail.CompareTo("luifa@scola.com") == 0);
                var nocioni = users.SingleOrDefault(x => x.Mail.CompareTo("chapu@nocioni.com") == 0);
                var campazzo = users.SingleOrDefault(x => x.Mail.CompareTo("facu@campazzo.com") == 0);

                #region Campazzo's Voting
                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.KeyPlayer,
                    VotingUserId = campazzo.Id,
                    VotingUserName = campazzo.Name,
                    VotedUserId = ginobili.Id,
                    VotedUserName = ginobili.Name
                });

                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.TeamPlayer,
                    VotingUserId = campazzo.Id,
                    VotingUserName = campazzo.Name,
                    VotedUserId = scola.Id,
                    VotedUserName = scola.Name
                });

                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.Funny,
                    VotingUserId = campazzo.Id,
                    VotingUserName = campazzo.Name,
                    VotedUserId = nocioni.Id,
                    VotedUserName = nocioni.Name
                });
                #endregion

                #region Nocioni's Voting
                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.Funny,
                    VotingUserId = nocioni.Id,
                    VotingUserName = nocioni.Name,
                    VotedUserId = campazzo.Id,
                    VotedUserName = campazzo.Name
                });

                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.KeyPlayer,
                    VotingUserId = nocioni.Id,
                    VotingUserName = nocioni.Name,
                    VotedUserId = ginobili.Id,
                    VotedUserName = ginobili.Name
                });

                _context.VoteDbSet.Add(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Nomination = VotesRestApi.Core.Enums.Nomination.TeamPlayer,
                    VotingUserId = nocioni.Id,
                    VotingUserName = nocioni.Name,
                    VotedUserId = scola.Id,
                    VotedUserName = nocioni.Name
                });
                #endregion

                await _context.SaveChangesAsync();
            }
        }

        private async Task Validate(Vote vote)
        {
            var users = await _userContext.UserDbSet.ToListAsync();

            if(!users.Any())
            {
                throw new Exception("No Employee found.");
            }

            var votingUser = users.SingleOrDefault(x => x.Id == vote.VotingUserId);

            if (votingUser == null)
            {
                throw new Exception("Voting Employee not found.");
            }

            var votedUser = users.SingleOrDefault(x => x.Id == vote.VotedUserId);
            
            if (votedUser == null)
            {
                throw new Exception("Voted Employee not found.");
            }

            if (votingUser.Id == votedUser.Id)
            {
                throw new Exception("Voting and Voted Employee can't be the same person.");
            }

            var votes = await _context.VoteDbSet.ToListAsync();

            if(votes.Any())
            {
                int year = vote.Date.Year;
                int month = vote.Date.Month;

                bool existSameVote = votes.Any(x => x.Date.Year == year
                                                && x.Date.Month == month
                                                && x.VotingUserId == vote.VotingUserId
                                                && x.Nomination == vote.Nomination);

                if(existSameVote)
                {
                    throw new Exception(string.Format("Exist the same vote for: Employee: {0}, Year: {1}, Month: {2}, Nomination:{3}", votingUser.Name, vote.Date.ToString("yyyy"), vote.Date.ToString("MM"), vote.NominationDescription));
                }
            }
        }

        private bool VoteExists(Guid id)
        {
            return _context.VoteDbSet.Any(e => e.Id == id);
        }
    }
}
