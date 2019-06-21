using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.ServiceInterface
{
    public interface IMovieService
    {

        List<MovieEntity> GetPageList(int pageIndex, int pageSize, out int count);

        List<MovieEntity> GetPageListByTag(int pageIndex, int pageSize, out int count, string sort);

        List<MovieEntity> GetSortPageList(int pageIndex, int pageSize, out int count, string sort);

        List<MovieEntity> GetPageListByKey(int pageIndex, int pageSize, out int count, string key);

        List<MovieEntity> GetTopMovies(int count);

        List<MovieEntity> GetMoviesByKeyWords(string keyWords);

        MovieEntity GetEntity(int id);

        bool Add(MovieEntity movie);

        bool Update(MovieEntity movie);

        bool Delte(MovieEntity movie);
    }
}
