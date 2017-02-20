using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public enum ServiceType
    {

    }

    /// <summary>
    /// 服务流程项目
    /// </summary>
    public class ServiceDescription : BaseModel
    {
        public ServiceType Type { get; set; }
        public string Name { get; set; }
        public string Config { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<ServicePackage> Packages { get; private set; }
    }
    /// <summary>
    /// 服务套餐
    /// </summary>
    public class ServicePackage : BaseModel
    {

        public virtual ICollection<ServiceDescription> Services { get; private set; }

        public string Name { get; set; }

        public string Remark { get; set; }
    }
}