using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace F5QI.SMS.Web.Models
{
    public class SMSUser : IdentityUser<long, SMSUserLogin, SMSUserRole, SMSUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SMSUserManager manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class SMSRole : IdentityRole<long, SMSUserRole> { }
    public class SMSUserLogin : IdentityUserLogin<long> { }
    public class SMSUserRole : IdentityUserRole<long> { }
    public class SMSUserClaim : IdentityUserClaim<long> { }     
    public class SMSUserStore : UserStore<SMSUser, SMSRole, long, SMSUserLogin, SMSUserRole, SMSUserClaim>
    {
        public SMSUserStore(DbContext context) : base(context)
        {
        }
    }

}