using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using MyMovie.Core.Entities;
using MyMovie.Core.ServiceInterface;
using MyMovie.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyMovie.WebApplication.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        private readonly ICommentService _commentService;

        private readonly IArticleService _articleService;

        public UserController(IUserService userService, ICommentService commentService, IArticleService articleService)
        {
            _userService = userService;
            _commentService = commentService;
            _articleService = articleService;
        }





        /// <summary>
        /// LoginPage
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// SignUpPage
        /// </summary>
        /// <returns></returns>
        [Route("signup")]
        public ActionResult SignUp()
        {
            return View();
        }



        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [Route("access_token")]
        [HttpPost]
        public ActionResult GetAccessToken()
        {
            string userName = Request.Form["userName"].ToString();
            string password = Request.Form["password"].ToString();
            UserEntity user = _userService.GetEntity(userName, password);
            if (user == null)
            {
                return Json(new { StatusCode = 404, Message = "Error in UserName Or Password" });
            }
            //var claims = new List<Claim>() {
            //    new Claim(ClaimTypes.Name,user.UserName),
            //    new Claim("NickName",user.NickName),
            //    new Claim(ClaimTypes.Sid,user.Id.ToString())
            //};
            //var identity = new ClaimsIdentity(claims);
            //ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            //HttpContext.User = principal;

            Session["User"] = user;
            return Json(new { StatusCode = 200, Message = "Success" });
        }


        /// <summary>
        /// 保存注册信息
        /// </summary>
        /// <returns></returns>
        [Route("signup/post")]
        [HttpPost]
        public ActionResult SignUpPost()
        {
            string userName = Request.Form["userName"].ToString();
            string nickName = Request.Form["nickName"].ToString();
            string password = Request.Form["password"].ToString();

            UserEntity user = new UserEntity
            {
                UserName = userName,
                Password = password,
                NickName = nickName,
                BirthDay = DateTime.Now
            };

            // 用户名存在
            if (_userService.GetEntity(userName)!=null)
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.NoContent, Message = $"the UserName {userName} has exits" });
            }

            if (_userService.AddUser(user))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = "An Error occured when saving UserEntity" });
            }
        }


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        public ActionResult LogOut()
        {
            Session["User"] = null;
            return Redirect("/");
        }



        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        [Route("info")]
        public ActionResult UserInfo()
        {

            UserEntity user = Session["User"] as UserEntity;
            ViewBag.User = user;
            return View();
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [Route("update")]
        public ActionResult Update()
        {
            string nickName = Request.Form["nickName"].ToString();
            string telNumber = Request.Form["telNumber"].ToString();
            string bio = Request.Form["bio"].ToString();
            string gender = Request.Form["gender"].ToString();
            UserEntity user = Session["User"] as UserEntity;

            user.NickName = nickName;
            user.TelNumber = telNumber;
            user.Bio = bio;
            user.Gender = gender;

            if (_userService.UpdateUser(user))
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return Json(new ResultModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"An Error Occues When Saving UserEntity" });
            }
        }



        [Route("center/{userName}")]
        public ActionResult UserCenter()
        {
            string userName = RouteData.Values["userName"].ToString();

            UserEntity user = _userService.GetEntity(userName);

            ViewBag.User = user;

            List<CommentEntity> comments = _commentService.GetEntitiesByUserId(user.Id);
            if (comments.Count > 5)
            {
                comments = comments.Take(5).ToList();
            }

            List<ArticleEntity> reviews = _articleService.GetEntitiesByUserId(user.Id);
            if (reviews.Count > 5)
            {
                reviews = reviews.Take(5).ToList();
            }
            ViewBag.Comments = comments;
            ViewBag.Reviews = reviews;

            return View();
        }
    }
}