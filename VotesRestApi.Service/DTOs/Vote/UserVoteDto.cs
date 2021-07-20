using System;
using System.Collections.Generic;
using System.Text;

namespace VotesRestApi.Service.DTOs
{
    public class UserVoteDto
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
    }
}
