using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F5QI.SMS.Web.Areas.Admin.Models
{
    public class AdminMenu
    {
        public static IEnumerable<MenuItem> GetMenu(Controller controller)
        {
            return new[]
                {
                    new MenuItem {
                        Title="主菜单",
                        IsLable=true
                    },
                    new MenuItem {
                        Title="首页",
                        Url= controller.Url.Action(nameof(Controllers.HomeController.Index)),
                        IcoClass="fa fa-circle-o"
                    },
                    new MenuItem {
                        Title="用户相关",
                        IcoClass="fa fa-users",
                        Url="#" ,
                        Child=new[] { 
                            new MenuItem {
                                Title="用户管理",
                                Url=controller.Url.Action(nameof(Controllers.HomeController.UserManege)),
                                IcoClass="fa fa-circle-o"
                            }
                        }
                    },
                    new MenuItem {
                        Title="合同管理",
                        IcoClass="fa fa-users",
                        Url="#"
                    },
                    new MenuItem {
                        Title="系统设置",
                        IcoClass="fa fa-users",
                        Url="#" ,
                        Child=new[] {
                            new MenuItem {
                                Title="服务管理",
                                Url=controller.Url.Action(nameof(Controllers.HomeController.UserManege)),
                                IcoClass="fa fa-circle-o"
                            },
                            new MenuItem {
                                Title="套餐管理",
                                Url=controller.Url.Action(nameof(Controllers.HomeController.UserManege)),
                                IcoClass="fa fa-circle-o"
                            }
                        }
                    }
                };
        }

    }
}