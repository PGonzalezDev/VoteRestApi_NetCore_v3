using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.Enums;
using VotesRestApi.Core.Helper;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Interfaces;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Service.Interfaces;

namespace VotesRestApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<GetUserResultDto> GetAll()
        {
            var users = _userRepository.GetAll();
            
            return users?.Select(x => new GetUserResultDto(x));
        }

        public async Task<GetUserResultDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return (user != null) ? new GetUserResultDto(user) : null;
        }

        public async Task<Guid> AddAsync(CreateUserDto dto)
        {
            var id = await _userRepository.AddAsync(new User()
            { 
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Mail = dto.Mail,
                Pass = CryptographyHelper.Encrypt(dto.Pass),
                UserRoleValue = (int)UserRol.Player,
            });

            return id;
        }

        public async Task<bool> UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            bool hasNewPass = !string.IsNullOrWhiteSpace(dto.NewPass);
            bool isValid = true;

            if (hasNewPass)
            {
                isValid = user.Pass.Equals(CryptographyHelper.Encrypt(dto.OldPass));
            }

            if (isValid)
            {
                user.Name = !string.IsNullOrWhiteSpace(dto.Name) ? dto.Name : user.Name;
                user.Mail = !string.IsNullOrWhiteSpace(dto.Mail) ? dto.Mail : user.Mail;
                user.Pass = (hasNewPass) ? CryptographyHelper.Encrypt(dto.NewPass) : user.Pass;

                await _userRepository.UpdateAsync(user);
            }

            return isValid;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            bool exist = await _userRepository.AnyAsync(x => x.Id == id);

            if(exist)
            {
                var user = await _userRepository.GetByIdAsync(id);
                await _userRepository.RemoveAsync(user);
            }

            return exist;
        }
    }
}
