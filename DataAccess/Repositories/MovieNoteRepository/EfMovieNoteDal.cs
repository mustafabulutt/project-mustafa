using Core.DataAccess.EntityFramework;
using DataAccess.Context.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieNoteRepository
{
    public class EfMovieNoteDal : EfEntityRepositoryBase<MovieNote, InContext>, IMovieNoteDal
    {
        public List<MovieNote> GetByMovieUserNote(int userId, long movieId)
        {
            using (var context = new InContext())
            {
               List<MovieNote> result = context.MovieNote.Where(x => x.UserId == userId && x.MovieId == movieId).ToList();

                return result;
            }
        }
    }
}
