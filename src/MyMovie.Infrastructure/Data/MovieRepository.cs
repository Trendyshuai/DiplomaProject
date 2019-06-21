using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class MovieRepository : Repository<MovieEntity>, IMovieRepository
    {
        public MovieRepository(EfDbContext efDbContext) : base(efDbContext)
        {

        }
    }
}
