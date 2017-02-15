using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class PaymentRecord:BaseModel
    {
        public long Id { get; set; }

        public string SecurityStamp { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentState State { get; set; }
        public string Code { get { return $"PaymentRecord_{Id}"; } }
        public string ThirdPartyCode { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public enum PaymentState
    {
    }

    public enum PaymentMethod
    {
    }
}