﻿using F5QI.SMS.Web.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace F5QI.SMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var ef = SMSDbContext.Create();
            ef.Database.CreateIfNotExists();
            ef.Database.Initialize(true);
            Newtonsoft.Json.JsonConvert.DefaultSettings = () =>
            {
                var s= new Newtonsoft.Json.JsonSerializerSettings();
                s.Converters.Add(new StringEnumConverter());
                return s;
            };
        }
    }
}
