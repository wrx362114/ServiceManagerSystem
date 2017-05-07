using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models.DTO
{
    public class ServicePackageModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public string Name { get; set; }
        public List<ServicesModel> Services { get; set; }

        public class ServicesModel
        {
            public long ServiceId { get; set; }
            public decimal Price { get; set; }
            public int SequenceNumber { get; set; }
        }
    }
}