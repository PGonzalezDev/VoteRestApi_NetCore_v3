using Microsoft.EntityFrameworkCore;
using VotesRestApi.Core.Models;

namespace VotesRestApi.Service.Context
{
    public class VoteContext : DbContext
    {
        public VoteContext(DbContextOptions<VoteContext> options)
            : base(options)
        {
        }

        public DbSet<Vote> VoteDbSet { get; set; }
    }
}
