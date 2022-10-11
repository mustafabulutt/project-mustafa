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



        /// <summary>
        /// Film Listelerini sayfalama yaparak sorgulamanıza yarar
        /// </summary>
        /// <remarks>
        /// Film Listesini sorgulayabilirsiniz
        /// </remarks>
        /// <param name="pageSize">100</param>
        /// <param name="pageNumber">1</param>
      
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


        /// <summary>
        /// film id gönderip film hakkında tüm detaylara ve sorguladıgınız filmin ortalama puanına , eklediğiniz notlara ulaşabileceğiniz endpoint
        /// </summary>
        /// <remarks>
        /// Film detaylarını sorgulayabilirsiniz
        /// </remarks>
        /// <param name="movieId">2</param>
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


        /// <summary>
        /// Mail Adresinize size önerebileceğimiz film listesini gönderir
        /// </summary>
        /// <remarks>
        /// Öneri filmleri mail oalrak alma
        /// </remarks>
        /// <param name="email">info@myemail.com</param>
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
