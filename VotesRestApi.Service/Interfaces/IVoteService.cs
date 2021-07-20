using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;

namespace VotesRestApi.Service.Interfaces
{
    public interface IVoteService
    {
        IEnumerable<GetVoteResultDto> GetAll();
        GetVoteResultDto GetByIdAsync(Guid id);
        Task<Guid?> AddAsync(CreateVoteDto dto);
        Task<bool> UpdateAsync(UpdateVoteDto dto);
        Task<bool> RemoveAsync(Guid id);
    }
}
