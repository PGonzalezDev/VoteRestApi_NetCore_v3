using System;

namespace VotesRestApi.Core.DTOs
{
    public class UpdateVoteDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public Guid? VotedUserId { get; set; }
        public int? Nomination { get; set; }
    }
}
