using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class SMSDbContent :
        IdentityDbContext<
            SMSUser,
            IdentityRole<long, IdentityUserRole<long>>,
            long,
            IdentityUserLogin<long>,
            IdentityUserRole<long>,
            IdentityUserClaim<long>
            >
    {
        public SMSDbContent()
            : base("DefaultConnection")
        {
        }

        public static SMSDbContent Create()
        {
            return new SMSDbContent();
        }
    }
}