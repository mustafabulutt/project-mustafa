using Business.Utilities.ApiRequest;
using Core.Utilities.Result.Concrete;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IApiRequest _iApiRequest;

        public ServiceController(IApiRequest apiRequest)
        {
            _iApiRequest = apiRequest;
        }


        /// <summary>
        /// Film listelerini periyodik olarak ve manuel olarak uzak sunucudan çekmek için kullanılır
        /// </summary>
        /// <remarks>
        /// Film Listelerini uzak sunucudan çeker.
        /// </remarks>
      
        [HttpGet("StartJobService")]
        public IActionResult StartJobService()
        {
            _iApiRequest.GetMovie();
            

            RecurringJob.AddOrUpdate<IApiRequest>("GetMovie", t => t.GetMovie(), Cron.Daily(10,00));
            return Ok( new SuccessResult("Servis Başlatıldı"));

        }
    }
}
