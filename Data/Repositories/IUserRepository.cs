using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tandem.API.Data.dbos;
using Tandem.API.Data.dtos;

namespace Tandem.API.Data.Repositories
{
    public interface IUserRepository
    {
        Task<GetUserResponseDto> GetUser(string email);

        Task SaveUser(User user);

        Task DeleteUser(string email);
    }
}
