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
    public class CommentService : ICommentService
    {

        private readonly ICommentRepository _commentRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMovieRepository _movieRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }



        public List<CommentEntity> GetPageList(int pageIndex, int pageSize, out int count, string key)
        {
            List<CommentEntity> list = _commentRepository.LoadPageEntities(pageIndex, pageSize, out count, m => m.Conetnt.Contains(key), m => m.CreateDate, false).ToList();

            foreach (CommentEntity c in list)
            {
                c.MovieName = _movieRepository.GetById(c.MovieId).Name;
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.ConvertDate = c.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return list;
        }


        public bool AddEntity(CommentEntity entity)
        {
            return _commentRepository.Add(entity);
        }


        public List<CommentEntity> GetEntities(int movideId, int count, out int total)
        {
            total = _commentRepository.List(c => c.MovieId == movideId).Count();
            List<CommentEntity> list = _commentRepository.List(c => c.MovieId == movideId).OrderByDescending(m => m.CreateDate).Take(count).ToList();

            foreach (CommentEntity c in list)
            {
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.UserName = _userRepository.GetById(c.UserId).UserName;
            }
            return list;
        }


        public List<CommentEntity> GetEntities(int movieId)
        {

            List<CommentEntity> list = _commentRepository.List(c => c.MovieId == movieId).OrderByDescending(m => m.CreateDate).ToList();
            foreach (CommentEntity c in list)
            {
                c.NickName = _userRepository.GetById(c.UserId).NickName;
                c.UserName = _userRepository.GetById(c.UserId).UserName;
            }
            return list;
        }


        public List<CommentEntity> GetEntitiesByUserId(int userId)
        {
            List<CommentEntity> entities = _commentRepository.List(c => c.UserId == userId).ToList();
            foreach (CommentEntity c in entities)
            {
                c.Movie = _movieRepository.GetById(c.MovieId);
                c.UserName = _userRepository.GetById(c.UserId).UserName;
            }
            return entities;
        }


        public CommentEntity GetEntity(int id)
        {
            return _commentRepository.GetById(id);
        }


        public bool Delete(CommentEntity entity)
        {
            return _commentRepository.Delete(entity);
        }

        public bool Update(CommentEntity entity)
        {
            return _commentRepository.Update(entity);
        }

    }
}
