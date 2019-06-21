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
    [RoutePrefix("reviews")]
    public class ReviewsController : Controller
    {

        private readonly IArticleService _articleService;

        private readonly IMovieService _movieService;

        public ReviewsController(IArticleService articleService, IMovieService movieService)
        {
            _articleService = articleService;
            _movieService = movieService;
        }

        #region 视图

        [Route("{movieId}")]
        public ActionResult Index()
        {
            int movieId = Convert.ToInt32(RouteData.Values["movieId"]);
            ViewBag.Movie = _movieService.GetEntity(movieId);
            ViewBag.Reviews = _articleService.GetEntities(movieId);
            return View();
        }


        /// <summary>
        /// 影评详情
        /// </summary>
        /// <returns></returns>
        [Route("detail/{id}")]
        public ActionResult Detail()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            ViewBag.Review = _articleService.GetEntity(id);
            return View();
        }


        /// <summary>
        /// 我的影评
        /// </summary>
        /// <returns></returns>
        [Login]
        [Route("")]
        public ActionResult MyReviews()
        {
            UserEntity user = Session["User"] as UserEntity;
            ViewBag.Reviews = _articleService.GetEntitiesByUserId(user.Id);
            return View();
        }
        #endregion



        /// <summary>
        /// 保存影评
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ValidateInput(false)]
        public ActionResult Add()
        {
            if (Session["User"] == null)
            {
                return Redirect("/user/login");
            }
            UserEntity user = Session["User"] as UserEntity;
            string content = Request.Form["content"].ToString().Trim();
            int movieId = Convert.ToInt32(Request.Form["movieId"]);
            string title = Request.Form["title"].ToString().Trim();
            string text = Request.Form["text"].ToString().Trim();
            ArticleEntity article = new ArticleEntity
            {
                Title = title,
                Content = content,
                UserId = user.Id,
                MovieId = movieId,
                Text = text,
                IsAnonymous = 1
            };
            if (_articleService.AddEntity(article))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when saving ArticleEntity" });
            }
        }



        [HttpPost]
        [Route("get")]
        public ActionResult GetComment()
        {
            int id = Convert.ToInt32(Request.Form["id"]);

            ArticleEntity comment = _articleService.GetEntity(id);

            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The ArticleEntity id {id} is not found" });
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

            ArticleEntity comment = _articleService.GetEntity(id);

            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The ArticleEntity id {id} is not found" });
            }

            if (_articleService.Delete(comment))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }

            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when deteting ArticleEntity" });
            }
        }


        [ValidateInput(false)]
        [HttpPost]
        [Route("update")]
        public ActionResult Update()
        {

            int id = Convert.ToInt32(Request.Form["id"]);
            string content = Request.Form["content"].ToString().Trim();
            string text = Request.Form["text"].ToString().Trim();
            string title = Request.Form["title"].ToString().Trim();

            ArticleEntity comment = _articleService.GetEntity(id);
            comment.Title = title;
            comment.Content = content;
            comment.Text = text;
            if (comment == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The ArticleEntity id {id} is not found" });
            }

            if (_articleService.Update(comment))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }

            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when updating ArticleEntity" });
            }
        }
    }
}