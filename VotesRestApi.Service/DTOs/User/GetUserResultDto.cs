namespace VotesRestApi.Service.DTOs
{
    public class GetUserResultDto : BaseDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        
        public GetUserResultDto() { }

        public GetUserResultDto(Core.Models.User user)
        {
            if (user != null)
            {
                this.Id = user.Id;
                this.Name = user.Name;
                this.Mail = user.Mail;
            }
        }
    }
}
