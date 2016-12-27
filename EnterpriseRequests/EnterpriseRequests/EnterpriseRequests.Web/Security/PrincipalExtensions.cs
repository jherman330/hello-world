using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;

namespace EnterpriseRequests.Web.Security
{
    //TODO: update this using statement with the fully qualified project namespace and move it out of the namespace declaration
    using Models;

    public static class PrincipalExtensions
    {
#if _IMPERSONATION_

        private static List<string> _ImpersonatedRoles = null;
        
        public static List<string> ImpersonatedRoles
        {
            get
            {
                if (_ImpersonatedRoles == null)
                {
                    string impersonatedRoles = ConfigurationManager.AppSettings["ImpersonatedRoles"];

                    _ImpersonatedRoles = new List<string>();

                    if (!string.IsNullOrEmpty(impersonatedRoles))
                    {
                        foreach (string role in impersonatedRoles.Split(";".ToCharArray()))
                        {
                            _ImpersonatedRoles.Add(role.Trim().ToUpper());
                        }
                    }
                }

                return _ImpersonatedRoles;
            }
            set
            {
                if (value != null)
                {
                    _ImpersonatedRoles = new List<string>();
                    foreach (string role in value)
                    {
                        _ImpersonatedRoles.Add(role.ToUpper());
                    }
                }
                else
                {
                    _ImpersonatedRoles = null;
                }
            }
        }
        
        public static bool IsInAnyRole(this IPrincipal principal, IEnumerable<string> roles)
        {
            return roles.Any(role => ImpersonatedRoles.Contains(role.ToUpper()));
        }
        
        public static bool IsAdmin(this IPrincipal principal)
        {
            return ImpersonatedRoles.Contains(Roles.Admin.ToUpper());
        }

#else

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(Roles.Admin);
        }

        public static bool IsInAnyRole(this IPrincipal principal, IEnumerable<string> roles)
        {
            return roles.Any(principal.IsInRole);
        }

#endif

        public static bool IsInAnyRole(this IPrincipal principal, string roles, char delimiter)
        {
            return IsInAnyRole(principal, roles.Split(delimiter).AsEnumerable());
        }

        public static bool IsInAnyRole(this IPrincipal principal, string commaSeperatedRoles)
        {
            return IsInAnyRole(principal, commaSeperatedRoles, Convert.ToChar(","));
        }

        public static CachedUser Employee(this IPrincipal principal)
        {
            return UserCacheManager.GetCachedUser(principal.Identity.Name);
        }
    }
}