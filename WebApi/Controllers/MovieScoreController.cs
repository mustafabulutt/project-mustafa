using Business.Repositories.MovieScoreRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieScoreController : ControllerBase
    {
        private readonly IMovieScoreService _movieScoreService;

        public MovieScoreController(IMovieScoreService movieScoreService)
        {
            _movieScoreService = movieScoreService;
        }


        [HttpGet("GetMovieMyScore")]
        public IActionResult GetMovieMyScore(int movieId)
        {


            var result = _movieScoreService.GetByMovieUserScore(movieId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("GetMovieMeanScore")]
        public IActionResult GetMovieMeanScore(int movieId)
        {


            var result = _movieScoreService.GetMovieMeanScore(movieId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpPost("AddMovieScore")]
        public IActionResult AddMovieScore(MovieScore score)
        {
            var result = _movieScoreService.Add(score);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }


    }
}
