using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tandem.API.Data.sql
{
    public static class Sql
    {
        public const string GetUser = @"SELECT * From dbo.Users where EmailAddress=@EmailAddress";

        public const string SaveUser = @"INSERT INTO dbo.Users ([UserId], [FirstName], [MiddleName], [LastName],
                                    [EmailAddress], [PhoneNumber]) 
                                VALUES(@UserId, @FirstName, @MiddleName, @LastName, @EmailAddress, @PhoneNumber)";

    }
}
