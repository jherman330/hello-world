using System.Web.Mvc;

namespace EnterpriseRequests.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
