using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;

namespace EnterpriseRequests.Web.Security
{
    //TODO: update this using statement with the fully qualified project namespace and move it out of the namespace declaration
    using Models;

    public static class UserCacheManager
    {
        private static void CacheUserInfo(string login)
        {
            login = StripUserLogin(login);
            CachedUser user = new CachedUser(login);
            string empLookup = ConfigurationManager.AppSettings["EmployeeLookupURL"];

            if (string.IsNullOrWhiteSpace(empLookup))
            {
                //fail safe is to put empty object into cache so anything that tries to read from cache doesn't need special error checking too
                //Error view is an example, since it is tied to the _Layout page (which causes this method to be called), the Error view will not render
                HttpContext.Current.Cache.Insert(login, user, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                throw new ConfigurationErrorsException("EmployeeLookupURL application setting is not defined.");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(empLookup);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(string.Concat("employees/v1/", login, "/simple")).Result;

                if (response.IsSuccessStatusCode)
                {
                    user = response.Content.ReadAsAsync<CachedUser>().Result;
                }
            }

            HttpContext.Current.Cache.Insert(login, user, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

        public static CachedUser GetCachedUser(string login)
        {
            login = StripUserLogin(login);
            var user = HttpContext.Current.Cache.Get(login) as CachedUser;
            if (user == null)
            {
                CacheUserInfo(login);
                user = HttpContext.Current.Cache.Get(login) as CachedUser;
            }
            return user;
        }

        private static string StripUserLogin(string login)
        {
            return login.Split('\\').Last();
        }
    }
}