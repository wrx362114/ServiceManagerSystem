using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class OperationRecord : BaseModel
    {
        public long Id { get; set; }

        public string SecurityStamp { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public OperationType Type { get; set; }

        public long BusinessId { get; set; }

        public string Params { get; set; }
    }

    public enum OperationType
    {
    }
}