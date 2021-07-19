using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Repositories.Interfaces;
using VotesRestApi.Service.DTOs;
using VotesRestApi.Service.Interfaces;

namespace VotesRestApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<GetUserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            
            return users?.Select(x => new GetUserDto(x));
        }
    }
}
