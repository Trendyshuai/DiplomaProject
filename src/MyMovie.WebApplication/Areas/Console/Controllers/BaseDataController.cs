using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Filter;
using MyMovie.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Areas.Console.Controllers
{
    [Admin]
    public class BaseDataController : Controller
    {

        private readonly IDictionaryService _dictionaryService;

        public BaseDataController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        #region 视图
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            string action = Request.QueryString["action"].ToString();

            if (action == "update")
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                DictionaryEntity entity = _dictionaryService.GetEntity(id);
                ViewBag.Dic = entity;
                ViewBag.ParId = entity.ParId;
            }

            if (action == "add")
            {
                int parId = Convert.ToInt32(Request.QueryString["parId"]);
                ViewBag.ParId = parId;
            }

            ViewBag.Action = action;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetParentList()
        {
            List<DictionaryEntity> list = _dictionaryService.GetParentList();
            List<TreeModel> trees = new List<TreeModel>();
            foreach (var d in list)
            {
                trees.Add(new TreeModel { id = d.Id, text = d.Name });
            }

            List<TreeModel> result = new List<TreeModel>
            {
                new TreeModel { id = 0, text = "字典数据", state = "open", children = trees }
            };
            return Json(result);
        }


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="parId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(int parId, int rows, int page)
        {
            List<DictionaryEntity> list = _dictionaryService.GetPageList(rows, page, parId, out int count);
            return Json(new { rows = list, total = count }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 保存数据
        [HttpPost]
        public ActionResult AddDictionaryData()
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            string text = Request.Form["text"].ToString().Trim();
            int parId = Convert.ToInt32(Request.Form["parId"]);

            DictionaryEntity entity = new DictionaryEntity
            {
                Name = text,
                Abbreviation = string.Empty,
                ParId = parId,
                DelFlag = 1
            };

            if (_dictionaryService.AddEntity(entity))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when saving DictionaryEntity" });
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDictionaryData()
        {

            int id = Convert.ToInt32(Request.Form["id"]);
            string text = Request.Form["text"].ToString().Trim();
            // int parId = Convert.ToInt32(Request.Form["parId"]);

            DictionaryEntity entity = _dictionaryService.GetEntity(id);
            entity.Name = text;

            if (_dictionaryService.UpdateEntity(entity))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when saving DictionaryEntity" });
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete()
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            DictionaryEntity entity = _dictionaryService.GetEntity(id);
            if (entity == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = $"The entity id {id} is not found" });
            }
            if (_dictionaryService.Delete(entity))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error Occued when deleting DictionaryEntity" });
            }
        }

        #endregion
    }
}