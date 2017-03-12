using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Common
{
    public class ApiResult
    {
        public bool IsOk { get; set; }
        public string Msg { get; set; }
        public static ApiResult Ok()
        {
            return new ApiResult { IsOk = true };
        }
        public static ApiResult Fail(string msg)
        {
            return new ApiResult { IsOk = false, Msg = msg };
        }
    }
}