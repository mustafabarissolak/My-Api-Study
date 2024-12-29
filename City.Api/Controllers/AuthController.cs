using City.Api.Core;
using City.Api.Core.Dtos.User;
using City.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace City.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authRepository.Register(new UserEntity
            { UserName = request.Username }, request.Password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLogin request)
        {
            var response = await _authRepository.Login(request.Username, request.Password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
