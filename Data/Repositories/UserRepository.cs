using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tandem.API.Data.dbos;
using Tandem.API.Data.dtos;
using Tandem.API.Data.sql;

namespace Tandem.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }

        public async Task<GetUserResponseDto> GetUser(string email)
        {
            using (var conn = new SqlConnection(_appSettings.ConnectionStrings.TandemDB))
            {
                var result = await conn.QueryFirstOrDefaultAsync<User>(Sql.GetUser, new { EmailAddress = email });
                if (result == null)
                    return null;

                return new GetUserResponseDto()
                {
                    Name = GetName(result.FirstName, result.MiddleName, result.LastName),
                    EmailAddress = result.EmailAddress,
                    PhoneNumber = result.PhoneNumber
                };
            }
        }

        private string GetName(string firstName, string middleName, string lastName)
        {
            if(!string.IsNullOrEmpty(middleName))
            {
                return firstName + " " + middleName + " " + lastName;
            } else
            {
                return firstName + " " + lastName;
            }
        }

    }
}
