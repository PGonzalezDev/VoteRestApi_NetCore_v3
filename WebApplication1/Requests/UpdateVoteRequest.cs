using System;
using VotesRestApi.Core.DTOs;

namespace WebApplication.Requests
{
    public class UpdateVoteRequest
    {
        public string Comment { get; set; }
        public Guid? VotedUserId { get; set; }
        public int? Nomination { get; set; }

        public UpdateVoteDto ToDto(Guid id)
        {
            return new UpdateVoteDto()
            {
                Id = id,
                Comment = this.Comment,
                VotedUserId = this.VotedUserId,
                Nomination = this.Nomination
            };
        }
    }
}
