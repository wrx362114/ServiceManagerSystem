using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace F5QI.SMS.Web.Models
{
    public class SMSUser : IdentityUser<long, IdentityUserLogin<long>, IdentityUserRole<long>, IdentityUserClaim<long>>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<SMSUser, long> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}