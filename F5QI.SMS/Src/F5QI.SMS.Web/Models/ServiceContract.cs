using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class ServiceContract : BaseModel
    {

        public string Name { get; set; }

        /// <summary>
        /// 甲方
        /// </summary>
        public long FirstPartyUserId { get; set; }

        /// <summary>
        /// 乙方
        /// </summary>
        public long SecondPartyUserId { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<ServiceContractPaymentPlan> PaymentPlans { get; private set; }

        public virtual ICollection<ServiceContractJobConfig> PlanJobs { get; private set; }

    }

    public class ServiceContractJobConfig : BaseModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ServiceContract Contract { get; set; }
        public long ContractId { get; set; }

        public long ServiceId { get; set; }
        public string Config { get; set; }
        /// <summary>
        /// 办事人ID
        /// </summary>
        public long ClerkId { get; set; }

        public SMSUser Clerk { get; set; }

        public JobState State { get; set; }

        public JobType Type { get; set; }
    }

    public enum JobType
    {
    }

    public class ServiceContractTemplate : BaseModel
    {
        public string Name { get; set; }

        public ServiceContractTemplateType Type { get; set; }

        public string Config { get; set; }
    }


    public class ServiceContractPaymentPlan : BaseModel
    {
        public DateTime PlanPayTime { get; set; }

        public long ContractId { get; set; }
        public ServiceContract Contract { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }

        public virtual ICollection<PaymentRecord> Records { get; private set; } 
    }


    public enum JobState
    {
    }
    public enum ServiceContractTemplateType
    {
    }
}