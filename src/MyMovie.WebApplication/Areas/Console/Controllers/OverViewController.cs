using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Areas.Console.Controllers
{
    public class OverViewController : Controller
    {

        private readonly IUserService _userService;

        public OverViewController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Console/OverView
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login()
        {
            string userName = Request.Form["userName"].ToString();

            string password = Request.Form["password"].ToString();

            AdminEntity admin = _userService.GetAdmin(userName, password);
            if (admin == null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error in UserName Or Password" });
            }
            Session["Admin"] = admin;
            return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
        }


        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            return Redirect("/Console/OverView");
        }
    }
}