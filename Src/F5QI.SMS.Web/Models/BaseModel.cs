using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            SecurityStamp = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        public long Id { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}