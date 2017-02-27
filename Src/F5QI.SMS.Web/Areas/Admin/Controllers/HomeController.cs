using F5QI.SMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace F5QI.SMS.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var model = new IndexViewModel(this)
            {
                CurrentPage = "后台首页",
                HeadImgUrl = "",
                LoginUserName = "测试"
            };
            return View(model);
        }
        public ActionResult ServiceManege()
        {
            var model = new ServiceManegeViewModel(this)
            {
                CurrentPage = "服务管理",
                HeadImgUrl = "",
                LoginUserName = "测试"
            };
            return View(model);
        }
        public ActionResult UserManege()
        {
            var model = new UserManegeViewModel(this)
            {
                CurrentPage = "后台首页",
                HeadImgUrl = "",
                LoginUserName = "测试"
            };
            return View(model);
        }
    }
}