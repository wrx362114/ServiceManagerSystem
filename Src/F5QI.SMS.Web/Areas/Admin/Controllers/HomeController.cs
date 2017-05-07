using F5QI.SMS.Web.Areas.Admin.Models;
using F5QI.SMS.Web.Common;
using F5QI.SMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace F5QI.SMS.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private SMSDbContext Db { get; set; }
        public HomeController()
        {
            Db = SMSDbContext.Create();
        }
        public HomeController(SMSDbContext db)
        {
            Db = db;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var model = new Models.IndexViewModel(this)
            {
                CurrentPage = "首页",
                HeadImgUrl = "",
                LoginUserName = "测试"
            };
            return View(model);
        }
        public ActionResult ServiceManege(int page = 1, int pagesize = 20)
        {
            var ros = Db.Services.OrderByDescending(a => a.Id).Skip((page - 1) * pagesize).Take(pagesize);
            var model = new ServiceManegeViewModel(this)
            {
                CurrentPage = "服务管理",
                HeadImgUrl = "",
                LoginUserName = "测试",
                RowCount = Db.Services.Count(),
                PageIndex = page,
                PageSize = pagesize,
                Services = ros.ToList()
            };
            return View(model);
        }
        public ActionResult ServicePackageManege(int page = 1, int pagesize = 20)
        {
            var ros = Db.ServicePackages
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * pagesize)
                .Take(pagesize);
            var model = new ServicePackageManegeViewModel(this)
            {
                CurrentPage = "服务套餐管理",
                HeadImgUrl = "",
                LoginUserName = "测试",
                RowCount = Db.Services.Count(),
                PageIndex = page,
                PageSize = pagesize,
                ServicePackages = ros.Select(a => new Web.Models.DTO.ServicePackageModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Remark = a.Remark,
                    Services = a.Services.Select(s => new Web.Models.DTO.ServicePackageModel.ServicesModel
                    {
                        Price = s.Price,
                        SequenceNumber = s.SequenceNumber,
                        ServiceId = s.ServiceId
                    }).ToList()
                }).ToList(),
                Services = Db.Services.ToList()
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