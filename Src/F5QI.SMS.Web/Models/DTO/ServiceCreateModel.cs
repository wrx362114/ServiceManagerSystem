using F5QI.SMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models.DTO
{
    public class ServiceCreateModel
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public ServiceType Type { get; set; }
        public decimal Price { get; set; }
        public string Config { get; set; }
    }
}