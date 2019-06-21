using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Controllers
{

    [RoutePrefix("")]
    public class HomeController : Controller
    {

        private readonly IUserService _userService;

        private readonly IMovieService _movieService;

        private readonly ICommentService _commentService;

        private readonly IArticleService _articleService;

        private readonly IDictionaryService _dictionaryService;

        public HomeController(IUserService userService,
            IMovieService movieService,
            ICommentService commentService,
            IArticleService articleService,
            IDictionaryService dictionaryService)
        {
            _userService = userService;
            _movieService = movieService;
            _articleService = articleService;
            _commentService = commentService;
            _dictionaryService = dictionaryService;
        }


        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>

        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Movies = _movieService.GetTopMovies(12);
            return View();
        }


        /// <summary>
        /// 电影详情
        /// </summary>
        /// <returns></returns>
        [Route("movie/{id}")]
        public ActionResult Details()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            MovieEntity movie = _movieService.GetEntity(id);
            if (movie != null)
            {
                ViewBag.Movie = movie;
                // 短评
                ViewBag.Comments = _commentService.GetEntities(movie.Id, 12, out int total);
                // 短评数量
                ViewBag.CommentCount = total;
                // 影评
                ViewBag.Reviews = _articleService.GetEntities(movie.Id, 10, out int reviewsCount);
                ViewBag.ReviewsCount = reviewsCount;
            }
            return View();
        }



        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        [Route("movie/type")]
        public ActionResult MoveType()
        {
            // 分类
            List<DictionaryEntity> list = _dictionaryService.GetParentList();
            foreach (var d in list)
            {
                d.SubList = _dictionaryService.GetEntitiesByparId(d.Id);
            }
            ViewBag.Dics = list;
            // 电影
            ViewBag.Movies = _movieService.GetPageList(1, 10, out int count);
            ViewBag.Count = count;
            return View();
        }


        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        [Route("movie/top")]
        public ActionResult TopMovie()
        {
            List<MovieEntity> list = _movieService.GetSortPageList(1, 10, out int count, "R");
            ViewBag.Movies = list;
            ViewBag.Count = count;
            return View();
        }



        [HttpGet]
        [Route("movie/chart")]
        public ActionResult GetTopMovie()
        {
            string[] range = Request.QueryString["range"].ToString().Split(',');
            int page = 0;
            int row = 0;
            for (int i = 0; i < range.Length; i++)
            {
                page = Convert.ToInt32(range[0]);
                row = Convert.ToInt32(range[1]);
            }
            // N 按名称，D 按时间， R按评分
            string sort = Request.QueryString["sort"].ToString();
            List<MovieEntity> list = _movieService.GetSortPageList(page, row, out int count, sort);
            return Json(new { rows = list, count }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 根据Tag筛选数据
        /// </summary>
        /// <returns></returns>
        [Route("movie/tag")]
        [HttpGet]
        public ActionResult GetMoviesByTag()
        {
            string tags = Request.QueryString["tags"].ToString();
            string[] range = Request.QueryString["range"].ToString().Split(',');
            int page = 0;
            int row = 0;
            for (int i = 0; i < range.Length; i++)
            {
                page = Convert.ToInt32(range[0]);
                row = Convert.ToInt32(range[1]);
            }
            List<MovieEntity> list = _movieService.GetPageListByTag(page, row, out int count, tags);
            return Json(new { rows = list, count }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        [Route("movie/search")]
        public ActionResult Search()
        {
            string keyWord = Request.QueryString["keyword"].ToString().Trim();
            ViewBag.Movies = _movieService.GetMoviesByKeyWords(keyWord);
            ViewBag.KeyWords = keyWord;
            return View();
        }



        /// <summary>
        /// 关于网站
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
    }
}