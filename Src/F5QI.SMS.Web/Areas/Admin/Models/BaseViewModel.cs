using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Areas.Admin.Models
{
    public class BaseViewModel
    {
        public BaseViewModel(System.Web.Mvc.Controller controller)
        {
            Menu = AdminMenu.GetMenu(controller);
        }
        private BaseViewModel() { }
        public string CurrentPage { get; set; }
        public string HeadImgUrl { get; set; }
        public string LoginUserName { get; set; }
        public IEnumerable<MenuItem> Menu { get; }
        public MenuItem CurrentMenu
        {
            get
            {
                foreach (var item in Menu)
                {
                    if (item.Title == CurrentPage)
                    {
                        return item;
                    }
                    if (item.IsActive(CurrentPage))
                    {
                        return item.Child.FirstOrDefault(a => a.Title == CurrentPage);
                    }
                }
                return new MenuItem
                {
                    Title="菜单配置错误",
                    Url="#"
                };
            }
        }
    }
    public class MenuItem
    {
        public MenuItem()
        {
            Child = Enumerable.Empty<MenuItem>();
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Url { get; set; }
        public string IcoClass { get; set; }
        public IEnumerable<MenuItem> Child { get; set; }
        public bool IsLable { get; set; }
        public bool IsActive(string currentPage)
        {
            return currentPage == Title || Child.Any(m => m.Title == currentPage);
        }
        public bool IsTree { get { return Child.Any(); } }
    }

}