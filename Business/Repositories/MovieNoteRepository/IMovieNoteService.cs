using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieNoteRepository
{
    public interface IMovieNoteService
    {
        IResult Add(MovieNote movieNote);

        IDataResult<List<MovieNote>> GetByMovieUserNote(long id);

    }
}
