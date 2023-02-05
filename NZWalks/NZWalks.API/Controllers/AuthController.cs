using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository repository, ITokenHandler tokenHandler)
        {
            this._repository = repository;
            this._tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            // Check if user is authenticate
            var user = await _repository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if(user == null)
            {
                return BadRequest("Username or poasswrd is incorrect");
            }

            // Return a Jwt token
            var token = await _tokenHandler.CreateToken(user);
            return Ok(token);
        }
    }
}
