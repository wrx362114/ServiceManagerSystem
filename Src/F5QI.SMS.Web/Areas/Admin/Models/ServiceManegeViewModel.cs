using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F5QI.SMS.Web.Areas.Admin.Models
{
    public class ServiceManegeViewModel : BaseViewModel
    {
        public ServiceManegeViewModel(Controller controller) : base(controller)
        {
        }
        public int PageCount
        {
            get
            {
                return RowCount / PageSize + RowCount % PageSize == 0 ? 0 : 1;
            }
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public List<Web.Models.ServiceDescription> Services { get; set; }
    }
}