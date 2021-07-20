using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VotesRestApi.Core.Enums;
using VotesRestApi.Core.Helper;
using VotesRestApi.Core.Models;

namespace VotesRestApi.Repositories.Configure
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> UserDbSet { get; set; }
        public DbSet<Vote> VoteDbSet { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            MockUsers();
            MockVotes();
        }

        private async void MockUsers()
        {
            bool haveUsers = await UserDbSet.AnyAsync();

            if (!haveUsers)
            {
                await UserDbSet.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Mail = "admin@admin.com",
                    Pass = CryptographyHelper.Encrypt("Admin00."),
                    UserRoleValue = 0
                });

                await UserDbSet.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manu Ginobili",
                    Mail = "manu@ginobili.com",
                    Pass = CryptographyHelper.Encrypt("Manu20."),
                    UserRoleValue = 1
                });

                await UserDbSet.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Chapu Nocioni",
                    Mail = "chapu@nocioni.com",
                    Pass = CryptographyHelper.Encrypt("Chapu13."),
                    UserRoleValue = 1
                });

                await UserDbSet.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Luis Scola",
                    Mail = "luifa@scola.com",
                    Pass = CryptographyHelper.Encrypt("Luifa04."),
                    UserRoleValue = 1
                });

                await UserDbSet.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Facu Campazzo",
                    Mail = "facu@campazzo.com",
                    Pass = CryptographyHelper.Encrypt("Facu07."),
                    UserRoleValue = 1
                });

                await SaveChangesAsync();
            }
        }

        private async void MockVotes()
        {
            var users = await UserDbSet.ToListAsync();
            bool hasVotes = await VoteDbSet.AnyAsync();

            if (users.Any() && !hasVotes)
            {
                var ginobili = users.SingleOrDefault(x => x.Mail.CompareTo("manu@ginobili.com") == 0);
                var scola = users.SingleOrDefault(x => x.Mail.CompareTo("luifa@scola.com") == 0);
                var nocioni = users.SingleOrDefault(x => x.Mail.CompareTo("chapu@nocioni.com") == 0);
                var campazzo = users.SingleOrDefault(x => x.Mail.CompareTo("facu@campazzo.com") == 0);

                #region Campazzo's Voting
                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.KeyPlayer,
                    VotingUserId = campazzo.Id,
                    //VotingUserName = campazzo.Name,
                    VotedUserId = ginobili.Id,
                    //VotedUserName = ginobili.Name
                });

                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.TeamPlayer,
                    VotingUserId = campazzo.Id,
                    //VotingUserName = campazzo.Name,
                    VotedUserId = scola.Id,
                    //VotedUserName = scola.Name
                });

                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.Funniest,
                    VotingUserId = campazzo.Id,
                    //VotingUserName = campazzo.Name,
                    VotedUserId = nocioni.Id,
                    //VotedUserName = nocioni.Name
                });
                #endregion

                #region Nocioni's Voting
                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.Funniest,
                    VotingUserId = nocioni.Id,
                    //VotingUserName = nocioni.Name,
                    VotedUserId = campazzo.Id,
                    //VotedUserName = campazzo.Name
                });

                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.KeyPlayer,
                    VotingUserId = nocioni.Id,
                    //VotingUserName = nocioni.Name,
                    VotedUserId = ginobili.Id,
                    //VotedUserName = ginobili.Name
                });

                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.TeamPlayer,
                    VotingUserId = nocioni.Id,
                    //VotingUserName = nocioni.Name,
                    VotedUserId = scola.Id,
                    //VotedUserName = nocioni.Name
                });
                #endregion

                #region Ginobili's Voting
                await VoteDbSet.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Nomination = Nomination.Funniest,
                    VotingUserId = ginobili.Id,
                    //VotingUserName = ginobili.Name,
                    VotedUserId = nocioni.Id,
                    //VotedUserName = nocioni.Name
                });
                #endregion

                await SaveChangesAsync();
            }
        }
    }
}
