using F5QI.SMS.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class PaymentRecord : BaseModel
    {
        public PaymentMethod Method { get; set; }

        public PaymentState State { get; set; }

        public PaymentType Type { get; set; }

        public decimal Amount { get; set; }

        public string Code { get { return $"F5QI.SMS.PaymentRecord_{Id}"; } }

        public string ThirdPartyCode { get; set; }
        public long PaymentPlanId { get; set; }
        public ServiceContractPaymentPlan PaymentPlan { get; set; }
    }
}