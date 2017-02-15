using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class ServiceContract : BaseModel
    {
        public long Id { get; set; }

        public string SecurityStamp { get; set; }

        public string Name { get; set; }

        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 甲方
        /// </summary>
        public long FirstPartyUserId { get; set; }
        /// <summary>
        /// 乙方
        /// </summary>
        public long SecondPartyUserId { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<ContractPaymentPlan> PaymentPlans { get; private set; }

    }

    public class ServiceJobConfig : BaseModel
    {
        public long Id { get; set; }

        public string SecurityStamp { get; set; }
        public long ContractId { get; set; }
        public long ServiceId { get; set; }
        public string Config { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 办事人ID
        /// </summary>
        public long ClerkId { get; set; }

        public JobState State{ get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }


    public class ContractPaymentPlan : BaseModel
    {
        public long Id { get; set; }

        public string SecurityStamp { get; set; }

        public DateTime PlanPayTime { get; set; }

        public long ContractId { get; set; }

        public ServiceContract Contract { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }


    public class JobState
    {
    }
}