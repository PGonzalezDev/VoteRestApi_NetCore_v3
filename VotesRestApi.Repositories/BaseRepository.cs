using System.Threading.Tasks;
using VotesRestApi.Repositories.Configure;

namespace VotesRestApi.Repositories
{
    public abstract class BaseRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public BaseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        //public void
    }
}
