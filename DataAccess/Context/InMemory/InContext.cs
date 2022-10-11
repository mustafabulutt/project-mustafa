using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.InMemory
{
    public class InContext : DbContext
    {
        protected override void OnConfiguring
         (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MovieProject");
        }

      
        public DbSet<User>? Users { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
        public DbSet<Movie>? Movie { get; set; }
        public DbSet<MovieNote>? MovieNote { get; set; }
        public DbSet<MovieScore>? MovieScore { get; set; }

    }
}
