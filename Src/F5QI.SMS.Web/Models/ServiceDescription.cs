using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public enum ServiceType
    {
        [Display(Name = "未知")]
        Unknown,
        [Display(Name = "信息录入")]
        InputInfomation,

    }

    /// <summary>
    /// 服务项目
    /// </summary>
    public class ServiceDescription : BaseModel
    {
        public ServiceType Type { get; set; }
        public string Name { get; set; }
        public string Config { get; set; }
        public string Remark { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<R_ServiceDescription_ServicePackage> Packages { get; private set; }
    }
    /// <summary>
    /// 服务套餐
    /// </summary>
    public class ServicePackage : BaseModel
    {
        /// <summary>
        /// 套餐整体价格
        /// </summary>
        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public virtual ICollection<R_ServiceDescription_ServicePackage> Services { get; private set; }
    }

    public class R_ServiceDescription_ServicePackage : BaseModel
    {
        /// <summary>
        /// 服务在这个套餐中的价格
        /// </summary>
        public decimal Price { get; set; }
        public int SequenceNumber { get; set; }
        public long ServiceId { get; set; }
        public ServiceDescription Service { get; set; }
        public long PackageId { get; set; }
        public ServicePackage Package { get; set; }

    }
}