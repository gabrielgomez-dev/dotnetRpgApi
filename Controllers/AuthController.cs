using dotnetRpgApi.Dtos.User;
using dotnetRpgApi.Models;
using dotnetRpgApi.Repositories.AuthRepository;
using dotnetRpgApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRpgApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var newUser = new User { Username = request.Username };
            var password = request.Password;

            var response = await _authRepository.Register(newUser, password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var username = request.Username;
            var password = request.Password;

            var response = await _authRepository.Login(username, password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}