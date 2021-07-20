using System;
using System.ComponentModel.DataAnnotations;
using VotesRestApi.Core.Enums;

namespace VotesRestApi.Core.Models
{
    public class Vote : EntityBase
    {
        [Required]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        [Required]
        public Guid VotingUserId { get; set; }

        [Required]
        public Guid VotedUserId { get; set; }

        [Required]
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
