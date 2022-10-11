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



        /// <summary>
        /// Film id verilen filme verdiğimiz puanı getirir
        /// </summary>
        /// <remarks>
        /// Filme verdiğimiz notu getirir
        /// </remarks>
        /// <param name="movieId">1</param>
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


        /// <summary>
        /// Film id verilen filmin ortalama puanını hesaplar
        /// </summary>
        /// <remarks>
        /// Filmin ortalama puanını getirir
        /// </remarks>
        /// <param name="movieId">1</param>
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


        /// <summary>
        /// Filme Puan Verir 1 en düşük 10 en yüksek olacak şekilde
        /// </summary>
        /// <remarks>
        /// Filme puan verir
        /// </remarks>
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
