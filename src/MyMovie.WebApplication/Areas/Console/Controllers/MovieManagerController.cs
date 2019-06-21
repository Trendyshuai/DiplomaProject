using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Filter;
using MyMovie.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Areas.Console.Controllers
{

    /// <summary>
    /// 电影管理
    /// </summary>
    [Admin]
    public class MovieManagerController : Controller
    {

        private readonly IMovieService _movieService;

        public MovieManagerController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        #region 视图
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 表单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            string action = Request.QueryString["action"].ToString();
            int id = Convert.ToInt32(Request.QueryString["id"]);

            if (action == "update")
            {
                ViewBag.Movie = _movieService.GetEntity(id);
            }

            return View();
        }
        #endregion

        #region 获取数据



        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(int page, int rows)
        {

            string key = Request.QueryString["key"].ToString();
            List<MovieEntity> list = _movieService.GetPageListByKey(page, rows, out int count, key);
            return Json(new { rows = list, total = count }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 保存数据


        public ActionResult AddMovie()
        {
            MovieEntity movie = new MovieEntity
            {
                Name = Request.Form["Name"].ToString(),
                Director = Request.Form["Director"].ToString(),
                Language = Request.Form["Language"].ToString(),
                ReleaseDate = Request.Form["ReleaseDate"].ToString(),
                Duration = Request.Form["Duration"].ToString(),
                Alias = Request.Form["Alias"].ToString(),
                Description = Request.Form["Description"].ToString(),
                Rate = Convert.ToInt32(Request.Form["Rate"]),
                DelFlag = Convert.ToInt32(Request.Form["DelFlag"])
            };

            HttpPostedFileBase fileBase = Request.Files["PosterURL"];
            string fileEx = Path.GetExtension(fileBase.FileName);
            string fileName = Guid.NewGuid().ToString() + fileEx;
            string path = Server.MapPath("~/Images/") + fileName;
            if (fileBase != null)
            {
                fileBase.SaveAs(path);
                movie.PosterURL = fileName;
            }
            else
            {
                movie.PosterURL = string.Empty;
            }

            if (_movieService.Add(movie))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when inserting MovieEntity" });
            }
        }


        [ValidateInput(false)]
        public ActionResult UpdateMovie()
        {
            MovieEntity movie = _movieService.GetEntity(Convert.ToInt32(Request.Form["Id"]));
            if (movie == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "The MovieEntity is Not Found" });
            }
            movie.Name = Request.Form["Name"].ToString();
            movie.Director = Request.Form["Director"].ToString();
            movie.Language = Request.Form["Language"].ToString();
            movie.ReleaseDate = Request.Form["ReleaseDate"].ToString();
            movie.Duration = Request.Form["Duration"].ToString();
            movie.Alias = Request.Form["Alias"].ToString();
            movie.Description = Request.Form["Description"].ToString();
            movie.Rate = Convert.ToSingle(Request.Form["Rate"]);
            movie.DelFlag = Convert.ToInt32(Request.Form["DelFlag"]);

            HttpPostedFileBase fileBase = Request.Files["PosterURL"];
            string fileEx = Path.GetExtension(fileBase.FileName);
            string fileName = Guid.NewGuid().ToString() + fileEx;
            string path = Server.MapPath("~/Images/") + fileName;

            if (fileBase.ContentLength > 0)
            {
                fileBase.SaveAs(path);
                movie.PosterURL = fileName;
            }

            if (_movieService.Update(movie))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when updateting MovieEntity" });
            }
        }


        public ActionResult DeleteMovie()
        {
            MovieEntity movie = _movieService.GetEntity(Convert.ToInt32(Request.Form["Id"]));
            if (movie == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "The MovieEntity is Not Found" });
            }

            if (_movieService.Delte(movie))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when deleting MovieEntity" });
            }
        }

        #endregion
    }
}