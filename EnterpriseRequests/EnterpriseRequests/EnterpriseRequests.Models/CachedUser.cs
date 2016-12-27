namespace EnterpriseRequests.Models
{
    public class CachedUser
    {
        public CachedUser()
        {

        }

        public CachedUser(string login)
        {
            SAPId = login;
            DisplayName = login;
        }

        public string SAPId { get; set; }
        public string DisplayName { get; set; }
    }
}
