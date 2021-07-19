using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotesRestApi.Service.DTOs;

namespace VotesRestApi.Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<GetUserDto> GetAll();
        Task<GetUserDto> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(UpdateUserDto dto);
        Task<bool> RemoveAsync(Guid id);
    }
}
