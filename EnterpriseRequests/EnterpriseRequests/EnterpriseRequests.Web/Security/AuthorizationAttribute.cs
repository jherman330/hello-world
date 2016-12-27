using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace EnterpriseRequests.Web.Security
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public AuthorizationAttribute(params string[] roles)
            : base()
        {
            Roles = string.Join(",", roles).Trim();
        }

#if _IMPERSONATION_

        [ExcludeFromCodeCoverage]
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;

            if (!string.IsNullOrEmpty(Roles))
            {
                foreach (string role in Roles.Split(",".ToCharArray()))
                {
                    if (!string.IsNullOrEmpty(role) && PrincipalExtensions.ImpersonatedRoles.Contains(role.Trim().ToUpper()))
                    {
                        isAuthorized = true;
                        break;
                    }
                }
            }

            if (!isAuthorized)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "AccessDenied",
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };
            }
        }

#else

        [ExcludeFromCodeCoverage]
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                ActionResult actionResult = new ViewResult
                {
                    ViewName = "AccessDenied",
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };

                filterContext.Result = actionResult;
            }
        }
#endif
    }
}