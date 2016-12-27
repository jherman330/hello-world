using FirstEnergy.Logging;
using System.Web.Mvc;

namespace EnterpriseRequests.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IEventLogger Log;

        public BaseController()
        {
            Log = EventLogger.GetLogger(GetType());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Log.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}