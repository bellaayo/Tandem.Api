using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tandem.API.Data.dtos
{
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
    }
}
