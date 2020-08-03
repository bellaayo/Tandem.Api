using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tandem.API.Data.dtos;

namespace Tandem.API.Data.Repositories
{
    public interface IUserRepository
    {
        Task<GetUserResponseDto> GetUser(string email);
    }
}
