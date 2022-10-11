using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieScoreRepository
{
    public interface IMovieScoreDal : IEntityRepository<MovieScore>
    {
        
        MovieScore GetByMovieUserScore(int userId, long movieId);

        double GetMovieAverageScore(long id);

    }
}
