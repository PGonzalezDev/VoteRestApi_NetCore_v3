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
            var votes = _voteRepository.GetAllVotesDto();

            return votes?.Select(x => new GetVoteResultDto(x));
        }

        public GetVoteResultDto GetByIdAsync(Guid id)
        {
            var vote = _voteRepository.GetVoteDtoById(id);
            var result = vote != null ? new GetVoteResultDto(vote) : null;

            return result;
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

        public async Task<bool> UpdateAsync(UpdateVoteDto dto)
        {
            Vote vote = await _voteRepository.GetByIdAsync(dto.Id);
            bool isValid = (vote != null);
            
            isValid &= !dto.VotedUserId.HasValue || await _userRepository.AnyAsync(x => x.Id == dto.VotedUserId.Value);

            isValid &= !dto.Nomination.HasValue || Enum.IsDefined(typeof(Nomination), dto.Nomination);

            if (isValid)
            {
                vote.Comment = !string.IsNullOrWhiteSpace(dto.Comment) ? dto.Comment : vote.Comment;
                vote.VotedUserId = dto.VotedUserId ?? vote.VotedUserId;
                vote.Nomination = dto.Nomination.HasValue ? (Nomination)dto.Nomination : vote.Nomination;

                await _voteRepository.UpdateAsync(vote);
            }

            return isValid;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            bool exist = await _voteRepository.AnyAsync(x => x.Id == id);

            if (exist)
            {
                var vote = await _voteRepository.GetByIdAsync(id);
                await _voteRepository.RemoveAsync(vote);
            }

            return exist;
        }
    }
}
