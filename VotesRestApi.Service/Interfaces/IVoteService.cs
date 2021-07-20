using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotesRestApi.Service.DTOs;

namespace VotesRestApi.Service.Interfaces
{
    public interface IVoteService
    {
        IEnumerable<GetVoteResultDto> GetAll();
        Task<GetVoteResultDto> GetByIdAsync(Guid id);
    }
}
