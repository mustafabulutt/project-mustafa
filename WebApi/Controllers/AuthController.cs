using Business.Abstract;
using Entities.Dtos;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromForm]RegisterAuthDto authDto)
        {

            

          var result =  _authService.Register(authDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }


        [HttpPost("Login")]
        public IActionResult Login(LoginAuthDto authDto)
        {

            var result =  _authService.Login(authDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
