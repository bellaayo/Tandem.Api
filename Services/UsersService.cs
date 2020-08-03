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
    }
}
