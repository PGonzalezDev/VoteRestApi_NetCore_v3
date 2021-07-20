using System;

namespace VotesRestApi.Service.DTOs
{
    public class CreateVoteDto
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Comment { get; set; }
        public Guid VotingUserId { get; set; }
        public Guid VotedUserId { get; set; }
        public int Nomination { get; set; }
    }
}
