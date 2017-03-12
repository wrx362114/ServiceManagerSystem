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
    [RoutePrefix("api/Services")]
    public class ServicesController : ApiController
    {

        [Route(""), HttpPost]
        public ApiResult Add([FromBody]ServiceCreateModel model)
        {
            var ef = SMSDbContext.Create();
            ef.Services.Add(new ServiceDescription
            {
                CreateTime = DateTime.Now,
                Config = model.Config,
                Name = model.Name,
                Price = model.Price,
                Remark = model.Remark,
                SecurityStamp = Guid.NewGuid().ToString(),
                UpdateTime = DateTime.Now,
                Type = model.Type
            });
            ef.SaveChangesAsync();
            return ApiResult.Ok();
        }

        [Route("{id}/Delete"), HttpPost]
        public ApiResult Delete([FromUri]long id)
        {

            var ef = SMSDbContext.Create();
            var item = ef.Services.SingleOrDefault(a => a.Id == id);
            if (item == null)
            {
                return ApiResult.Fail("找不到要删除的服务");
            }
            ef.Services.Remove(item);
            ef.SaveChangesAsync();
            return ApiResult.Ok();
        }

        [Route("{id}/Change"), HttpPost]
        public ApiResult Change([FromUri]long id, [FromBody]ServiceCreateModel model)
        {
            var ef = SMSDbContext.Create();
            var item = ef.Services.SingleOrDefault(a => a.Id == id);
            if (item == null)
            {
                return ApiResult.Fail("找不到要删除的服务");
            }
            item.Name = model.Name;
            item.Price = model.Price;
            item.Remark = model.Remark;
            item.Type = model.Type;
            item.Config = model.Config;
            item.UpdateTime = DateTime.Now;
            item.SecurityStamp = Guid.NewGuid().ToString();
            ef.SaveChangesAsync();
            return ApiResult.Ok();
        }
    }
}
