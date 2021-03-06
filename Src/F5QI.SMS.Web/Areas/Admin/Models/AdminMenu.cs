﻿using System;
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
                        Title="首页",
                        Url= controller.Url.Action("Index","Home"),
                        IcoClass="fa fa-circle-o"
                    },
                    new MenuItem {
                        Title="用户相关",
                        IcoClass="fa fa-users",
                        Url=controller.Url.Action("User","Home") ,
                        Child=new[] {
                            new MenuItem {
                                Title="用户管理",
                                Url=controller.Url.Action("User","Home") ,
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
                        Url=controller.Url.Action("ServiceManege","Services") ,
                        Child=new[] {
                            new MenuItem {
                                Title="服务管理",
                                Url=controller.Url.Action("ServiceManege","Services") ,
                                IcoClass="fa fa-circle-o"
                            },
                            new MenuItem {
                                Title="服务套餐管理",
                                Url=controller.Url.Action("ServicePackageManege","Services"),
                                IcoClass="fa fa-circle-o"
                            }
                        }
                    }
                };
        }

    }
}