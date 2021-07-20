using System;
using VotesRestApi.Core.DTOs;

namespace WebApplication.Requests
{
    public class CreateVoteRequest
    {
        public string Comment { get; set; }
        public Guid VotingUserId { get; set; }
        public Guid VotedUserId { get; set; }
        public int Nomination { get; set; }

        public CreateVoteDto ToDto()
        {
            return new CreateVoteDto()
            {
                Comment = this.Comment,
                VotingUserId = this.VotingUserId,
                VotedUserId = this.VotedUserId,
                Nomination = this.Nomination,
            };
        }
    }
}
