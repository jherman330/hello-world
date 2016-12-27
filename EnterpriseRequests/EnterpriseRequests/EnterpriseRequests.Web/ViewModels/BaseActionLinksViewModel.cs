using System.Collections.Generic;

namespace EnterpriseRequests.Web.ViewModels
{
    public class BaseActionLinksViewModel
    {
        public BaseActionLinksViewModel()
        {
            Controller = "";
            IsCreateVisible = true;
            IsDeleteVisible = true;
            IsDetailsVisible = true;
            IsEditVisible = true;
            CustomLinks = new List<CustomLink>();
        }

        public string Controller { get; set; }
        public bool IsCreateVisible { get; set; }
        public bool IsEditVisible { get; set; }
        public bool IsDetailsVisible { get; set; }
        public bool IsDeleteVisible { get; set; }
        public List<CustomLink> CustomLinks { get; set; }
    }

    public class CustomLink
    {
        public CustomLink()
        {
            Address = string.Empty;
            Text = string.Empty;
            Icon = "bookmark";
        }
        public string Address { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
    }
}