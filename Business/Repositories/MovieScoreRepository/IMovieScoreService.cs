using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieScoreRepository
{
    public interface IMovieScoreService
    {

        IResult Add(MovieScore movieScore);


        IDataResult<MovieScore> GetByMovieUserScore(long id);

        IDataResult<double> GetMovieMeanScore(long id);

        

    }
}
