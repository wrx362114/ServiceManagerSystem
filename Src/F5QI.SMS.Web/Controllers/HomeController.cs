using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F5QI.SMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var rm = new Random();
            int x = 5, y = 200;
            var ars = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                var list = new List<int>();
                for (int j = 0; j < y; j++)
                {
                    list.Add(rm.Next());
                }
                list.Sort();
                for (int j = 0; j < y; j++)
                {
                    ars[i, j] = list[j];
                }
            }
            //1.第k大的数在没行k/x列到k列的区间.
            //2.建立一个行头尾顺序列表

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}