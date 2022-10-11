using Business.Repositories.MovieRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
           _movieService = movieService;
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int pageSize , int pageNumber)
        {         
            

            var result = _movieService.GetList(pageSize, pageNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("GetInfo")]
        public IActionResult GetInfo(long movieId)
        {


            var result =  _movieService.getMovieInfo(movieId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("MovieDiscover")]
        public IActionResult MovieDiscover(string email)
        {
            var result = _movieService.SendDiscoverMail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
