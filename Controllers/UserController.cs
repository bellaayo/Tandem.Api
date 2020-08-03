using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tandem.API.Data.dtos;
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.EmailAddress)
                || string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName))
            {
                return BadRequest("Missing data!");
            }
            if (!ValidateEmail(request.EmailAddress))
            {
                return BadRequest("Invalid Email Address.");
            }

            var user = await _userService.GetUser(request.EmailAddress);

            if (user != null)
            {
                return Conflict("Email is already in use.");
            }

            var userId = await _userService.CreateUser(request);

            return Ok(userId);
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