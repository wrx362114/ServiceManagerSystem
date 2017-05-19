using F5QI.SMS.Web.Common;
using F5QI.SMS.Web.Models;
using F5QI.SMS.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace F5QI.SMS.Web.Apitrollers
{
    [RoutePrefix("api/ServicePackages")]
    public class ServicePackagesController : ApiController
    {
        private SMSDbContext db;
        public ServicePackagesController()
        {
            db = SMSDbContext.Create();
        }
        [Route("Add"), HttpPost]
        public ApiResult Add([FromBody]ServicePackageModel model)
        {
            db.ServicePackages.Add(new ServicePackage
            {
                Name = model.Name,
                Price = model.Price,
                Remark = model.Remark,
            });
            db.SaveChangesAsync();
            return ApiResult.Ok();
        }

        [Route("{id}/Delete"), HttpPost]
        public ApiResult Delete([FromUri]long id)
        {
            var item = db.ServicePackages.SingleOrDefault(a => a.Id == id);
            if (item == null)
            {
                return ApiResult.Fail("找不到要删除的服务");
            }
            if (item.Services.Any())
            {
                foreach (var s in item.Services)
                {
                    db.R_Service_Package.Remove(s);
                }
            }
            db.ServicePackages.Remove(item);
            db.SaveChangesAsync();
            return ApiResult.Ok();
        }

        [Route("{id}/Change"), HttpPost]
        public ApiResult Change([FromUri]long id, [FromBody]ServicePackageModel model)
        {
            var item = db.ServicePackages.SingleOrDefault(a => a.Id == id);
            if (item == null)
            {
                return ApiResult.Fail("找不到要删除的服务");
            }
            item.Name = model.Name;
            item.Price = model.Price;
            item.Remark = model.Remark;
            item.UpdateTime = DateTime.Now;
            item.SecurityStamp = Guid.NewGuid().ToString();
            db.SaveChangesAsync();
            return ApiResult.Ok();
        }

        [Route("{id}/Service/Add"), HttpPost]
        public ApiResult AddService([FromUri]long id, [FromBody]ServicePackageModel.ServicesModel model)
        {
            var package = db.ServicePackages.SingleOrDefault(m => m.Id == id);
            if (package == null)
            {
                return ApiResult.Fail("找不到要修改套餐");
            }
            if (package.Services.Any(a => a.ServiceId == model.ServiceId))
            {
                return ApiResult.Fail("服务已经存在请先删除");
            }
            package.Services.Add(new R_ServiceDescription_ServicePackage
            {
                PackageId = id,
                Price = model.Price,
                SequenceNumber = model.SequenceNumber,
                ServiceId = model.ServiceId
            });
            db.SaveChanges();
            return ApiResult.Ok();
        }


        [Route("{id}/Service/{rid}/Delete"), HttpPost]
        public ApiResult DelService([FromUri]long id, [FromUri]long rid)
        {
            var r = db.R_Service_Package.SingleOrDefault(a => a.Id == id);
            if (r == null)
            {
                return ApiResult.Fail("找不到");
            }
            return ApiResult.Ok();
        }
    }
}
