namespace VotesRestApi.Service.DTOs
{
    public class GetUserDto : BaseDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        
        public GetUserDto() { }

        public GetUserDto(Core.Models.User user)
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
