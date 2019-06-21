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
    public class ArticleService : IArticleService
    {

        private readonly IArticleRepository _articleRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMovieRepository _movieRepository;

        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }


        public List<ArticleEntity> GetPageList(int pageIndex, int pageSize, out int count, string key)
        {
            List<ArticleEntity> list = _articleRepository.LoadPageEntities(pageIndex, pageSize, out count, m => m.Title.Contains(key) || m.Text.Contains(key), m => m.CreateDate, false).ToList();

            foreach (ArticleEntity c in list)
            {
                c.MovieName = _movieRepository.GetById(c.MovieId).Name;
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.UserName = _userRepository.GetById(c.UserId).UserName;
                c.ConvertDate = c.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return list;
        }



        public bool AddEntity(ArticleEntity entity)
        {
            return _articleRepository.Add(entity);
        }


        public List<ArticleEntity> GetEntities(int movideId, int count, out int total)
        {
            total = _articleRepository.List(c => c.MovieId == movideId).Count();
            List<ArticleEntity> list = _articleRepository.List(c => c.MovieId == movideId).OrderByDescending(m => m.CreateDate).Take(count).ToList();

            foreach (ArticleEntity c in list)
            {
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.UserName = _userRepository.GetById(c.UserId).UserName;
            }
            return list;
        }


        public List<ArticleEntity> GetEntities(int movideId)
        {
            List<ArticleEntity> list = _articleRepository.List(c => c.MovieId == movideId).ToList();

            foreach (ArticleEntity c in list)
            {
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.UserName = _userRepository.GetById(c.UserId).UserName;
            }
            return list;
        }


        public ArticleEntity GetEntity(int id)
        {
            ArticleEntity article = _articleRepository.GetById(id);
            article.NickName = _userRepository.GetById(article.UserId).NickName;
            return article;
        }


        public List<ArticleEntity> GetEntitiesByUserId(int userId)
        {
            List<ArticleEntity> articleEntities = _articleRepository.List(a => a.UserId == userId).ToList();
            foreach (ArticleEntity a in articleEntities)
            {
                a.Movie = _movieRepository.GetById(a.MovieId);
                a.UserName = _userRepository.GetById(a.UserId).UserName;
            }
            return articleEntities;
        }


        public bool Delete(ArticleEntity entity)
        {
            return _articleRepository.Delete(entity);
        }

        public bool Update(ArticleEntity entity)
        {
            return _articleRepository.Update(entity);
        }
    }
}
