using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class FieldDescription : BaseModel
    {
        public FieldType Type { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public virtual ICollection<FieldGroups> Groups { get; private set; }
    }
    public enum FieldType
    {
        Error,
        String,
        Int,
        Decimal,
        DateTime
    }


    public class FieldGroups : BaseModel
    {

        public string Name { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<FieldDescription> Fields { get; private set; }
        public virtual ICollection<SMSUser> Users { get; private set; }

    }
}