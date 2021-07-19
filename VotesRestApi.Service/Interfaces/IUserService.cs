using System.Collections.Generic;
using VotesRestApi.Service.DTOs;

namespace VotesRestApi.Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<GetUserDto> GetAll();
    }
}
