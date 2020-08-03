using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tandem.API.Data.dtos
{
    public class GetUserResponseDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
