using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        //private readonly VoteContext _context;
        //private readonly UserContext _userContext;
        //
        //public VotesController(VoteContext context, UserContext userContext)
        //{
        //    _context = context;
        //    _userContext = userContext;
        //}
        //
        //// GET: api/votes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Vote>>> GetVoteDbSet()
        //{
        //    //await MockVotes();
        //
        //    return await _context.VoteDbSet.ToListAsync();
        //}
        //
        //// GET: api/votes/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Vote>> GetVote(Guid id)
        //{
        //    var vote = await _context.VoteDbSet.FindAsync(id);
        //
        //    if (vote == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return vote;
        //}
        //
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
        //// PUT: api/votes/{id}
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVote(Guid id, Vote vote)
        //{
        //    if (id != vote.Id)
        //    {
        //        return BadRequest();
        //    }
        //
        //    _context.Entry(vote).State = EntityState.Modified;
        //
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VoteExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //
        //    return NoContent();
        //}
        //
        //// POST: api/votes
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Vote>> PostVote(Vote vote)
        //{
        //    vote.Id = Guid.NewGuid();
        //    vote.Date = DateTime.Now;
        //
        //    await Validate(vote);
        //
        //    _context.VoteDbSet.Add(vote);
        //    await _context.SaveChangesAsync();
        //
        //    return CreatedAtAction("GetVote", new { id = vote.Id }, vote);
        //}
        //
        //// DELETE: api/votes/{id}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Vote>> DeleteVote(Guid id)
        //{
        //    var vote = await _context.VoteDbSet.FindAsync(id);
        //    if (vote == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    _context.VoteDbSet.Remove(vote);
        //    await _context.SaveChangesAsync();
        //
        //    return vote;
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
    }
}
