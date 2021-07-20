using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Interfaces;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Service.Interfaces;
using VotesRestApi.Core.Enums;

namespace VotesRestApi.Service
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUserRepository _userRepository;

        public VoteService(
            IVoteRepository voteRepository,
            IUserRepository userRepository
        )
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<GetVoteResultDto> GetAll()
        {
            var votes = _voteRepository.GetAllVotes();

            return votes?.Select(x => new GetVoteResultDto(x));
        }

        public GetVoteResultDto GetByIdAsync(Guid id)
        {
            var vote = _voteRepository.GetVoteById(id);

            return new GetVoteResultDto(vote);
        }

        public async Task<Guid?> AddAsync(CreateVoteDto dto)
        {
            Guid? resultId = null;

            bool isValid = Enum.IsDefined(typeof(Nomination), dto.Nomination);

            isValid &= await _userRepository.AnyAsync(x => x.Id == dto.VotingUserId);
            isValid &= await _userRepository.AnyAsync(x => x.Id == dto.VotedUserId);

            if (isValid)
            {
                resultId = await _voteRepository.AddAsync(new Vote()
                {
                    Id = Guid.NewGuid(),
                    Date = dto.Date,
                    Comment = dto.Comment,
                    VotingUserId = dto.VotingUserId,
                    VotedUserId = dto.VotedUserId,
                    Nomination = (Nomination)dto.Nomination
                });
            }

            return resultId;
        }
    }
}
