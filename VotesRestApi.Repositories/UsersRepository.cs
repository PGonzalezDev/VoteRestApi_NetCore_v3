using VotesRestApi.Repositories.Configure;

namespace VotesRestApi.Repositories
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
