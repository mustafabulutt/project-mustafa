using Business.Aspects.Secured;
using Business.Repositories.MovieNoteRepository;
using Business.Repositories.MovieScoreRepository;
using Core.Aspects.Caching;
using Core.Contans;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.MovieRepository;
using DataAccess.Repositories.MovieScoreRepository;
using Entities.Concrete;
using Entities.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieRepository
{
    public class MovieManager : IMovieService
    {

        private readonly IMovieDal _movieDal;
        private readonly IMovieScoreService _movieScoreService;
        private readonly IMovieNoteService _movieNoteService;

        public MovieManager(IMovieDal movieDal, IMovieScoreService movieScoreService, IMovieNoteService movieNoteService)
        {
            _movieDal= movieDal;
            _movieScoreService= movieScoreService;
            _movieNoteService= movieNoteService;
        }

        [SecuredAspect()]
        [CacheAspect(60)]
        public IDataResult<List<Movie>> GetList(int getCount,int pageNumber)
        {

            return new SuccessDataResult<List<Movie>>(_movieDal.GetLimitAll(getCount, pageNumber),_movieDal.GelTotalCount());

        }

        [SecuredAspect()]
        public IDataResult<GetMovieInfoDto> getMovieInfo(long movieId)
        {
            try
            {
                GetMovieInfoDto dto = new GetMovieInfoDto();
                var request = (HttpWebRequest)WebRequest.Create($"https://api.themoviedb.org/3/movie/{movieId}?api_key=c6a6acca076ad81d4d2317122bfc918e&language=en-US");

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dto = JsonConvert.DeserializeObject<GetMovieInfoDto>(responseString);
                
                IDataResult<MovieScore> MyScore = _movieScoreService.GetByMovieUserScore(movieId);
                IDataResult<List<MovieNote>> MyNote = _movieNoteService.GetByMovieUserNote(movieId);
                IDataResult<double> MeanScore = _movieScoreService.GetMovieMeanScore(movieId);



                dto.MyScore = MyScore.Data == null ? null : MyScore.Data.Score;
                dto.MeanScore = _movieScoreService.GetMovieMeanScore(movieId)==null?0: _movieScoreService.GetMovieMeanScore(movieId).Data;
                dto.MyNotes = MyNote.Data.Count() > 0 ? MyNote.Data:null;


                return new SuccessDataResult<GetMovieInfoDto>(dto,"İşlem Başarılı");

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetMovieInfoDto>("Yanıt Gelmedi");
            }

        }



        public IResult SendDiscoverMail(string email)
        {

            List<Movie> MovieList = _movieDal.GetLimitAll(10,1);

            string movieName = "";

            foreach (var item in MovieList)
            {
                movieName += " -"+item.original_title+" - ";
            }
            if (MovieList.Count() <= 0)
            {
                return new ErrorResult("Mail Servisi başlatılmadı.");
            }

            var smtpClient = new SmtpClient(MailSmtp.smtpHost, MailSmtp.smtpPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            smtpClient.Credentials = new NetworkCredential(MailSmtp.email, MailSmtp.password); 
            var message = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(email, "Film Öneri Listeniz."), new System.Net.Mail.MailAddress(email, email));
            message.Body = movieName;
            smtpClient.Send(message);




            return new SuccessResult("Öneri Listemiz Gönderilmiştir.");
            
        }
    }
}
