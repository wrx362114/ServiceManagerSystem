using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    /// <summary>
    /// 用户企业信息.
    /// </summary>
    public class EnterpriseInfo : BaseModel
    {
        public string Name { get; set; }
        public long UserId{ get; set; }
        public SMSUser UserInfo { get; set; }

    }
}