namespace VotesRestApi.Core.DTOs
{
    public class GetVoteResultDto : BaseDto
    {
        public string Date { get; set; }
        public string Comment { get; set; }
        public string VotingUserName { get; set; }
        public string VotedUserName { get; set; }
        public string NominationDescription { get; set; }

        public GetVoteResultDto() { }

        public GetVoteResultDto(VoteDto vote)
        {
            if (vote != null)
            {
                Id = vote.Id;
                Date = vote.Date.ToString("yyyy/MM/dd");
                Comment = vote.Comment ?? string.Empty;
                VotingUserName = vote.VotingUser.Name;
                VotedUserName = vote.VotedUser.Name;
                NominationDescription = vote.NominationDescription;
            }
        }
    }
}
