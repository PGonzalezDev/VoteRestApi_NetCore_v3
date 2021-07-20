using System;
using VotesRestApi.Core.Enums;
using VotesRestApi.Core.Helper;

namespace VotesRestApi.Core.DTOs
{
    public class VoteDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public UserVoteDto VotingUser { get; set; }
        public UserVoteDto VotedUser { get; set; }
        public Nomination Nomination { get; set; }
        public string NominationDescription => NominationHelper.GetNominationDescription(Nomination);
    }
}
