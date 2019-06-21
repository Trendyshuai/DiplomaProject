using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Filter;
using MyMovie.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Controllers
{
    [RoutePrefix("comment")]
    public class CommentController : Controller
    {

        private readonly ICommentService _commentService;

        private readonly IMovieService _movieService;

        public CommentController(ICommentService commentService, IMovieService movieService)
        {
            _commentService = commentService;
            _movieService = movieService;
        }


        #region 视图
        /// <summary>
        /// 评论首页
        /// </summary>
        /// <returns></returns>
        [Route("{movieId}")]

        public ActionResult Index()
        {
            int movieId = Convert.ToInt32(RouteData.Values["movieId"]);
            ViewBag.Movie = _movieService.GetEntity(movieId);
            ViewBag.Comments = _commentService.GetEntities(movieId);
            return View();
        }


        /// <summary>
        /// 我的短评
        /// </summary>
        /// <returns></returns>
        [Login]
        [Route("")]
        public ActionResult MyComments()
        {
            UserEntity user = Session["User"] as UserEntity;
            ViewBag.Comments = _commentService.GetEntitiesByUserId(user.Id);
            return View();
        }
        #endregion






        #region 添加评论
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public ActionResult AddComment()
        {
            if (Session["User"] is UserEntity user)
            {
                CommentEntity comment = new CommentEntity
                {
                    Conetnt = Request.Form["content"].ToString(),
                    UserId = user.Id,
                    MovieId = Convert.ToInt32(Request.Form["movieId"]),
                    ParId = 0
                };
                if (_commentService.AddEntity(comment))
                {
                    return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
                }
                else
                {
                    return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occured When Saving Data" });
                }
            }
            else
            {
                return Redirect("");
            }
        }
        #endregion

        [HttpPost]
        [Route("get")]
        public ActionResult GetComment()
        {
            int id = Convert.ToInt32(Request.Form["id"]);

            CommentEntity comment = _commentService.GetEntity(id);

            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The CommentEntity id {id} is not found" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success", Data = comment });
            }
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete()
        {
            int id = Convert.ToInt32(Request.Form["id"]);

            CommentEntity comment = _commentService.GetEntity(id);

            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The CommentEntity id {id} is not found" });
            }

            if (_commentService.Delete(comment))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }

            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when deteting CommentEntity" });
            }
        }


        [HttpPost]
        [Route("update")]
        public ActionResult Update()
        {

            int id = Convert.ToInt32(Request.Form["id"]);
            string content = Request.Form["content"].ToString().Trim();
            CommentEntity comment = _commentService.GetEntity(id);
            comment.Conetnt = content;
            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The CommentEntity id {id} is not found" });
            }

            if (_commentService.Update(comment))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }

            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when updating CommentEntity" });
            }
        }
    }
}