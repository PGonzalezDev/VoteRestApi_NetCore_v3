using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Service.Interfaces;
using WebApplication.Requests;

namespace WebApplication1.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        //// GET: api/votes/adminId/{adminId}/report/period/{yyyy-MM}
        //[HttpGet("adminId/{adminId}/report/period/{period}")]
        //public async Task<ActionResult<Report>> GetReport(Guid adminId, DateTime period)
        //{
        //    var admin = await _userContext.UserDbSet.FindAsync(adminId);
        //
        //    if (admin == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    if(!admin.IsAdmin)
        //    {
        //        throw new ApplicationException("You don't have admin permissions.");
        //    }
        //
        //    var users = await _userContext.UserDbSet.ToListAsync();
        //    var votes = await _context.VoteDbSet.ToListAsync();
        //
        //    //if(!votes.Any())
        //    //{
        //    //    await MockVotes();
        //    //
        //    //    votes = await _context.VoteDbSet.ToListAsync();
        //    //}
        //
        //    votes = votes.Where(x => x.Date.Year == period.Year
        //                                && x.Date.Month == period.Month)
        //                .ToList();
        //
        //    var mostVotedEmployee = votes.GroupBy(x => x.VotedUserId)
        //                                    .Select(mv => new {
        //                                        Count = mv.Count(),
        //                                        Name = mv.First().VotedUserName,
        //                                        Period = mv.First().Date.Date,
        //                                        ID = mv.Key
        //                                    })
        //                                    .OrderByDescending(x => x.Count)
        //                                    .FirstOrDefault();
        //
        //    Report report = new Report();
        //    report.MostVotedEmployee = new Tuple<string, int>(mostVotedEmployee.Name, mostVotedEmployee.Count);
        //    report.Period = mostVotedEmployee.Period;
        //    report.RegisteredEmployeeCount = users.Count(x => !x.IsAdmin);
        //
        //    var votedEmployeePerNomination = votes.GroupBy(x => new { x.VotedUserId, x.Nomination })
        //                                            .Select(mv => new
        //                                            {
        //                                                Count = mv.Count(),
        //                                                Name = mv.First().VotedUserName,
        //                                                Nomination = mv.First().Nomination,
        //                                                NominationDesc = mv.First().NominationDescription
        //                                            })
        //                                            .OrderByDescending(x => x.Count)
        //                                            .ToList();
        //
        //    report.MostVotedEmployeeForNomination = new Dictionary<string, string>();
        //
        //    foreach (var item in Enum.GetValues(typeof(Nomination)))
        //    {
        //        Nomination nomination = (Nomination)item;
        //
        //        var mostVoted = votedEmployeePerNomination.FirstOrDefault(x => x.Nomination == nomination);
        //
        //        if(mostVoted != null)
        //        {
        //            report.MostVotedEmployeeForNomination.Add(mostVoted.Name, mostVoted.NominationDesc);
        //        }
        //    }
        //
        //    return new ActionResult<Report>(report);
        //}
        
        //
        //private async Task Validate(Vote vote)
        //{
        //    var users = await _userContext.UserDbSet.ToListAsync();
        //
        //    if(!users.Any())
        //    {
        //        throw new ApplicationException("No Employee found.");
        //    }
        //
        //    var votingUser = users.SingleOrDefault(x => x.Id == vote.VotingUserId);
        //
        //    if (votingUser == null)
        //    {
        //        throw new ApplicationException("Voting Employee not found.");
        //    }
        //
        //    var votedUser = users.SingleOrDefault(x => x.Id == vote.VotedUserId);
        //    
        //    if (votedUser == null)
        //    {
        //        throw new ApplicationException("Voted Employee not found.");
        //    }
        //
        //    if (votingUser.Id == votedUser.Id)
        //    {
        //        throw new ApplicationException("Voting and Voted Employee can't be the same person.");
        //    }
        //
        //    var votes = await _context.VoteDbSet.ToListAsync();
        //
        //    if(votes.Any())
        //    {
        //        int year = vote.Date.Year;
        //        int month = vote.Date.Month;
        //
        //        bool existSameVote = votes.Any(x => x.Date.Year == year
        //                                        && x.Date.Month == month
        //                                        && x.VotingUserId == vote.VotingUserId
        //                                        && x.Nomination == vote.Nomination);
        //
        //        if(existSameVote)
        //        {
        //            throw new ApplicationException(string.Format("Exist the same vote for: Employee: {0}, Year: {1}, Month: {2}, Nomination:{3}", votingUser.Name, vote.Date.ToString("yyyy"), vote.Date.ToString("MM"), vote.NominationDescription));
        //        }
        //    }
        //}
        //
        //private bool VoteExists(Guid id)
        //{
        //    return _context.VoteDbSet.Any(e => e.Id == id);
        //}

        private readonly IVoteService _service;

        public VotesController(IVoteService service)
        {
            _service = service;
        }

        // GET: api/votes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVoteResultDto>>> GetAllVotes()
        {
            var votes = _service.GetAll();

            return Ok(votes);
        }

        // GET: api/votes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVoteResultDto>> GetVote(Guid id)
        {
            var vote = _service.GetByIdAsync(id);
        
            if (vote == null)
            {
                return NotFound();
            }
        
            return Ok(vote);
        }

        // POST: api/votes
        [HttpPost]
        public async Task<ActionResult<Guid>> PostVote(CreateVoteRequest request)
        {
            Guid? id = await _service.AddAsync(request.ToDto());

            if(!id.HasValue)
            {
                return UnprocessableEntity();
            }
        
            return CreatedAtAction("PostVote", id.Value);
        }

        // PUT: api/votes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] UpdateVoteRequest request)
        {
            bool isValid = await _service.UpdateAsync(request.ToDto(id));

            if (!isValid)
            {
                return UnprocessableEntity();
            }

            return NoContent();
        }

        // DELETE: api/votes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            bool exist = await _service.RemoveAsync(id);

            if (!exist)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
