using Core.DataAccess.EntityFramework;
using DataAccess.Context.InMemory;
using DataAccess.Repositories.MovieRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieScoreRepository
{
    public class EfMovieScoreDal : EfEntityRepositoryBase<MovieScore, InContext>, IMovieScoreDal
    {
        public MovieScore GetByMovieUserScore(int userId, long movieId)
        {
            using (var context = new InContext())
            {
                var result = context.MovieScore.Where(x => x.UserId == userId && x.MovieId == movieId).LastOrDefault();

                return result;
            }
        }

        public double GetMovieAverageScore(long id)
        {
            using (var context = new InContext())
            {
                List<MovieScore> result = context.MovieScore.Where(x=>x.MovieId == id).ToList();
                int sayac = 0;
                int totalScore = 0;
                foreach (var item in result)
                {
                    totalScore += item.Score;
                    sayac++;
                }
                if (totalScore == 0)
                {
                    return 0;
                }
                else
                {

                    return totalScore / sayac;
                }
            }
        }
    }
}
