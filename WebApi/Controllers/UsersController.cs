using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetList")]
        public IActionResult GetList()
        {

            var result = _userService.GetList();

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {

            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }


        }


        [HttpPost("Update")]
        public IActionResult Update(User user)
        {

            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }


        }


        [HttpPost("Delete")]
        public IActionResult Delete(User user)
        {

            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }


        }


        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {

            var result = _userService.ChangePassword(userChangePasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }


        }





    }
}
