using Business.Aspects.Secured;
using Business.Repositories.MovieNoteRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieNoteController : ControllerBase
    {
        private readonly IMovieNoteService _movieNoteService;

        public MovieNoteController(IMovieNoteService movieNoteService)
        {
            _movieNoteService = movieNoteService;
        }



        /// <summary>
        /// Film id gönderdiğiniz filme eklediğiniz notu getirir
        /// </summary>
        /// <remarks>
        /// Kullanıcının filme eklediği notu getirir
        /// </remarks>
        /// <param name="movieId">1</param>
        [HttpGet("GetMyNote")]
        public IActionResult GetMyNote(int movieId)
        {
            var result = _movieNoteService.GetByMovieUserNote(movieId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }


        /// <summary>
        /// Filme kişisel notunuzu ekler
        /// </summary>
        /// <remarks>
        /// Filme not ekler
        /// </remarks>
        
        [HttpPost("GetMyNote")]
        public IActionResult AddMovieNote(MovieNote note)
        {
            var result = _movieNoteService.Add(note);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
