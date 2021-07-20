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
    }
}
