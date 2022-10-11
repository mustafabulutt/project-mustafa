using Business.Aspects.Secured;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Contans;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.MovieScoreRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieScoreRepository
{
    public class MovieScoreManager : IMovieScoreService
    {
        private readonly IMovieScoreDal _movieScoreDal;
        private IHttpContextAccessor _httpContextAccessor;
        public MovieScoreManager(IMovieScoreDal movieScoreDal)
        {
            _movieScoreDal = movieScoreDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        [ValidationAspect(typeof(MovieScoreValidator))]
        [SecuredAspect()]
        public IResult Add(MovieScore movieScore)
        {

            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            MovieScore exist = _movieScoreDal.Get(i => i.MovieId == movieScore.MovieId && i.UserId == Convert.ToInt32(user));
            if (exist != null)
            {
                return new ErrorResult("Daha Önce Not Verilmiş.");

            }
            movieScore.UserId = Convert.ToInt32(user);            
            _movieScoreDal.Add(movieScore);
            return new SuccessResult(Messages.Add);


        }

        [SecuredAspect()]
        public IDataResult<MovieScore> GetByMovieUserScore(long id)
        {

            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          

            return new SuccessDataResult<MovieScore>(_movieScoreDal.GetByMovieUserScore(Convert.ToInt32(user), id));
        }
        [SecuredAspect()]
        public IDataResult<double> GetMovieMeanScore(long id)
        {
            return new SuccessDataResult<double>(Convert.ToDouble(_movieScoreDal.GetMovieAverageScore(id)));
        }
    }
}
