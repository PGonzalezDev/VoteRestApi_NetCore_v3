using System;
using VotesRestApi.Core.Enums;

namespace VotesRestApi.Core.DTOs
{
    public class VoteDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public UserVoteDto VotingUser { get; set; }
        public UserVoteDto VotedUser { get; set; }
        public Nomination Nomination { get; set; }

        public string NominationDescription
        {
            get
            {
                string description = string.Empty;

                switch (Nomination)
                {
                    case Nomination.TeamPlayer:
                        description = "Team Player";
                        break;
                    case Nomination.TechnicalReferent:
                        description = "Technical Referent";
                        break;
                    case Nomination.KeyPlayer:
                        description = "Key Player";
                        break;
                    case Nomination.Motivator:
                        description = "Motivator";
                        break;
                    case Nomination.Funniest:
                        description = "Funny";
                        break;
                }

                return description;
            }
        }
    }
}
