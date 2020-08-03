using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tandem.API.Data.dtos;
using Tandem.API.Data.Repositories;

namespace Tandem.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<GetUserResponseDto> GetUser(string email)
        {
            return await _userRepository.GetUser(email);
        }

        public async Task<Guid> CreateUser(CreateUserRequestDto request)
        {
            var userId = Guid.NewGuid();

            //TODO : Add automapper
            await _userRepository.SaveUser(new Data.dbos.User
            {
                UserId = userId,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            });

            return userId;
        }

        public async Task DeleteUser(string email)
        {
            await _userRepository.DeleteUser(email);
        }
    }
}
