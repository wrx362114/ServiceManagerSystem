using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Models
{
    public class SMSDbContext :
        IdentityDbContext<
            SMSUser,
            SMSRole,
            long,
            SMSUserLogin,
            SMSUserRole,
            SMSUserClaim
            >
    {
        public SMSDbContext()
            : base("DefaultConnection")
        {
        }

        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }
    }
}