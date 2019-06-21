using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using MyMovie.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }



        public List<MovieEntity> GetPageList(int pageIndex, int pageSize, out int count)
        {
            return _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => true, m => m.Rate, true).ToList();
        }


        /// <summary>
        /// 根据tag模糊查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<MovieEntity> GetPageListByTag(int pageIndex, int pageSize, out int count, string tag)
        {
            return _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => m.Language.Contains(tag) || m.ReleaseDate.Contains(tag), m => m.Rate, true).ToList();
        }


        public List<MovieEntity> GetSortPageList(int pageIndex, int pageSize, out int count, string sort)
        {

            List<MovieEntity> list = null;
            switch (sort)
            {
                case "N": list = _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => true, m => m.Name, true).ToList(); break;
                case "D": list = _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => true, m => m.CreateDate, false).ToList(); break;
                case "R": list = _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => true, m => m.Rate, false).ToList(); break;
                default: list = _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => true, m => m.Rate, false).ToList(); break;
            }
            return list;
        }


        public List<MovieEntity> GetTopMovies(int count)
        {
            return _movieRepository.List(m => true).OrderByDescending(m => m.Rate).Take(count).ToList();
        }


        public List<MovieEntity> GetMoviesByKeyWords(string keyWords)
        {
            return _movieRepository.List(m => m.Name.Contains(keyWords) || m.Director.Contains(keyWords) || m.Alias.Contains(keyWords) || m.Description.Contains(keyWords)).ToList();
        }


        public List<MovieEntity> GetPageListByKey(int pageIndex, int pageSize, out int count, string key)
        {
            return _movieRepository.LoadPageEntities(pageIndex, pageSize, out count, m => m.Name.Contains(key) || m.Director.Contains(key) || m.Alias.Contains(key) || m.Description.Contains(key), m => m.Rate, true).ToList();
        }



        public MovieEntity GetEntity(int id)
        {
            return _movieRepository.GetById(id);
        }



        public bool Add(MovieEntity movie)
        {
            return _movieRepository.Add(movie);
        }

        public bool Update(MovieEntity movie)
        {
            return _movieRepository.Update(movie);
        }

        public bool Delte(MovieEntity movie)
        {
            return _movieRepository.Delete(movie);
        }

    }
}
