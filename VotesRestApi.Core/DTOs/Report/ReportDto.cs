﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VotesRestApi.Core.DTOs
{
    public class ReportDto
    {
        public DateTime Period { get; set; }
        public MostVotedPlayerDto MostVotedPlayer { get; set; }
        public string MostVotedEmployeeDescription => 
            string.Format(
                "The most voted Player is {0} with {1} total votes for period {2}",
                MostVotedPlayer.Name,
                MostVotedPlayer.VotesAmount,
                Period.ToString("yyyy-MM")
            );
        public int RegisteredEmployeeCount { get; set; }
        public Dictionary<string, string> MostVotedEmployeeForNomination { get; set; }
    }

    public class MostVotedPlayerDto
    {
        public string Name { get; set; }
        public int VotesAmount { get; set; }

        public MostVotedPlayerDto() { }

        public MostVotedPlayerDto(string name, int votesAmount)
        {
            Name = name;
            VotesAmount = votesAmount;
        }
    }
}
