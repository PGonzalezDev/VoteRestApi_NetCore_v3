using VotesRestApi.Service.DTOs;

namespace WebApplication
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Pass { get; set; }

        public CreateUserDto ToDto()
        {
            return new CreateUserDto()
            {
                Name = this.Name,
                Mail = this.Mail,
                Pass = this.Pass
            };
        }
    }
}
