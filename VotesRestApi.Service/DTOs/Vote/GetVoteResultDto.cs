using System;

namespace VotesRestApi.Service.DTOs
{
    public class GetVoteResultDto : BaseDto
    {
        public string Date { get; set; }
        public string Comment { get; set; }
        public string VotingUserName { get; set; }
        public string VotedUserName { get; set; }
        public string NominationDescription { get; set; }

        public GetVoteResultDto() { }

        public GetVoteResultDto(Core.Models.Vote vote)
        {
            if (vote != null)
            {
                Id = vote.Id;
                Date = vote.Date.ToString("yyyy/MM/dd");
                Comment = vote.Comment ?? string.Empty;
                //VotingUserName = vote.VotingUserName;
                //VotedUserName = vote.VotedUserName;
                NominationDescription = vote.NominationDescription;
            }
        }
    }
}
