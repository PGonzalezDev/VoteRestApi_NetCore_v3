using System;
using VotesRestApi.Service.DTOs;

namespace WebApplication
{
    public class GetUserByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public GetUserByIdResponse() { }

        public GetUserByIdResponse(GetUserDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Mail = dto.Mail;
        }
    }
}
