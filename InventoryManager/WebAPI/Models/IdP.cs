namespace WebAPI.Models
{
    public class IdP : IIdentityProvider
    {
        public bool IsAuthorized(string token)
        {
            return true;
        }

        public bool IsAuthenticated(string token)
        {
            return true;
        }
    }
}