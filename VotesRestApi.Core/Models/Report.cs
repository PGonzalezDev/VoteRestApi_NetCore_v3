using System;
using System.Collections.Generic;

namespace VotesRestApi.Core.Models
{
    public class Report
    {
        public DateTime Period { get; set; }

        public Tuple<string, int> MostVotedEmployee { get; set; }

        public string MostVotedEmployeeDescription
        {
            get { return string.Format("The most voted Employee is {0} with {1} total votes for period {2}", MostVotedEmployee.Item1, MostVotedEmployee.Item2, Period.ToString("yyyy-MM")); }
        }

        public int RegisteredEmployeeCount { get; set; }

        public Dictionary<string, string> MostVotedEmployeeForNomination{ get; set; }


    }
}
