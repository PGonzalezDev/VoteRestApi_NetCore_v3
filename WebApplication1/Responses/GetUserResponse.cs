using System;
using VotesRestApi.Service.DTOs;

namespace WebApplication
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public GetUserResponse() { }

        public GetUserResponse(GetUserResultDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Mail = dto.Mail;
        }
    }
}
