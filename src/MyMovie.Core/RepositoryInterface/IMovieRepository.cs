using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.RepositoryInterface
{
    public interface IMovieRepository : IRepository<MovieEntity>
    {

    }
}
