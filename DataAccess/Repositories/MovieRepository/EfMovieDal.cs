using Core.DataAccess.EntityFramework;
using DataAccess.Context.InMemory;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieRepository
{
    public class EfMovieDal : EfEntityRepositoryBase<Movie, InContext>, IMovieDal
    {
        public int GelTotalCount()
        {
            using (var context = new InContext())
            {

                return context.Movie.Count() ;
            }
        }

        public List<Movie> GetLimitAll(int count,int pageNumber)
        {
            using (var context = new InContext())
            {
                
                
                


                if (pageNumber == 0)
                    pageNumber = 1;

                if (count == 0)
                    count = int.MaxValue;

                var skip = (pageNumber - 1) * count;

                List<Movie> nextPage = context.Movie.OrderBy(b => b.Id).Skip(skip).Take(count).ToList();

                return nextPage;
            }

        }

       
            

        
    }
}
