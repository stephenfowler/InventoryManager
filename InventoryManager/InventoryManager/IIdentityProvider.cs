namespace InventoryManager
{
    public interface IIdentityProvider
    {
        bool IsAuthorized(string token);
        bool IsAuthenticated(string token);
    }
}