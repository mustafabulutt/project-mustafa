using Business.Aspects.Secured;
using Business.Repositories.MovieNoteRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Contans;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Repositories.MovieNoteRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieNoteRepository
{
    public class MovieNoteManager : IMovieNoteService
    {
        private readonly IMovieNoteDal _repository;
        private readonly IUserDal _userDal;
        private IHttpContextAccessor _httpContextAccessor;

        public MovieNoteManager(IMovieNoteDal repository, IUserDal userDal)
        {
            _repository = repository;
            _userDal = userDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }
        [ValidationAspect(typeof(MovieNoteValidator))]
        [SecuredAspect()]
        public IResult Add(MovieNote movieNote)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            movieNote.UserId = Convert.ToInt32(user);
            _repository.Add(movieNote);
            return new SuccessResult(Messages.Add);
        }


        [SecuredAspect()]
        public IDataResult<List<MovieNote>> GetByMovieUserNote(long id)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           

            return new SuccessDataResult<List<MovieNote>>(_repository.GetByMovieUserNote(Convert.ToInt32(user),id));

        }
    }
}
