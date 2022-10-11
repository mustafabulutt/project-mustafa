using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieRepository
{
    public interface IMovieDal : IEntityRepository<Movie>
    {

        List<Movie> GetLimitAll(int count,int pageNumber);
        int GelTotalCount();


    }
}
