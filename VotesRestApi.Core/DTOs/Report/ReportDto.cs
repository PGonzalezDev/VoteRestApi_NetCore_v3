using System;
using System.Collections.Generic;
using System.Text;

namespace VotesRestApi.Core.DTOs
{
    public class ReportDto
    {
        private DateTime _period { get; set; }

        public string Period => _period.ToString("yyyy-MM");

        public MostVotedPlayerDto MostVotedPlayer { get; set; }
        public string MostVotedEmployeeDescription => 
            string.Format(
                "The most voted Player is {0} with {1} total votes for period {2}",
                MostVotedPlayer.Name,
                MostVotedPlayer.VotesAmount,
                Period
            );
        public int RegisteredEmployeeCount { get; set; }
        public Dictionary<string, string> MostVotedEmployeeForNomination { get; set; }

        public ReportDto() { }

        public ReportDto(DateTime period)
        {
            _period = period;
        }
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
