using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieRepository
{
    public interface IMovieService
    {
        IDataResult<List<Movie>> GetList(int getCount,int pageNumber);

        IDataResult<GetMovieInfoDto> getMovieInfo(long movieId);

        IResult SendDiscoverMail(string email);

    }
}
