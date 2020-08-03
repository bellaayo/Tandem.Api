using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tandem.API.Services;

namespace Tandem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery]string email)
        {
            if (string.IsNullOrEmpty(email) || !ValidateEmail(email))
            {
                return BadRequest("Email Address is not valid!");
            }

            var response = await _userService.GetUser(email);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //TODO: This should be moved to a validation service class
        private bool ValidateEmail(string email)
        {
            var pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            return isValid;
        }
    }
}