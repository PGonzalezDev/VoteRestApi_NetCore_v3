namespace VotesRestApi.Core.DTOs
{
    public class UpdateUserDto : BaseDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string NewPass { get; set; }
        public string OldPass { get; set; }
    }
}
