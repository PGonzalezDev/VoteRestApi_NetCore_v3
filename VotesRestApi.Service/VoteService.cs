using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Interfaces;
using VotesRestApi.Service.DTOs;
using VotesRestApi.Service.Interfaces;

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
            var votes = _voteRepository.GetAll();

            return votes?.Select(x => new GetVoteResultDto(x));
        }

        public async Task<GetVoteResultDto> GetByIdAsync(Guid id)
        {
            var vote = await _voteRepository.GetByIdAsync(id);

            return new GetVoteResultDto(vote);
        }

        public async Task<Guid> AddAsync(CreateVoteDto dto)
        {
            Guid id = await _voteRepository.AddAsync(new Vote()
            {
                Id = Guid.NewGuid(),
                Date = dto.Date,
                Comment = dto.Comment,
                VotingUserId = dto.VotingUserId,
                VotedUserId = dto.VotedUserId,
                Nomination = (Core.Enums.Nomination)dto.Nomination
            });

            return id;
        }
    }
}
