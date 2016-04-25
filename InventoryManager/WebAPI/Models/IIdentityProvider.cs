namespace WebAPI.Models
{
    public interface IIdentityProvider
    {
        bool IsAuthorized(string token);
        bool IsAuthenticated(string token);
    }
}