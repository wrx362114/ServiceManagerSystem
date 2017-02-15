using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public interface BaseModel
    {
        long Id { get; set; }
        string SecurityStamp { get; set; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
    }
}