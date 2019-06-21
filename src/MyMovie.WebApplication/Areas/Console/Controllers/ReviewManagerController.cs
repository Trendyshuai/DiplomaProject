using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Areas.Console.Controllers
{
    [Admin]
    public class ReviewManagerController : Controller
    {

        private readonly IArticleService _articleService;
        public ReviewManagerController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        #region 视图
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(int page, int rows)
        {
            string key = Request.QueryString["key"].ToString();
            List<ArticleEntity> list = _articleService.GetPageList(page, rows, out int count, key);

            return Json(new { rows = list, total = count }, JsonRequestBehavior.AllowGet);
        }
    }
}