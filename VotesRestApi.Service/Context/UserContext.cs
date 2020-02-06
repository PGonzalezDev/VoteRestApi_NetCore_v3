using Microsoft.EntityFrameworkCore;
using VotesRestApi.Core.Models;

namespace VotesRestApi.Service.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserDbSet { get; set; }
    }
}
