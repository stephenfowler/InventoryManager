namespace WebAPI.Models
{
    public interface INotifier
    {
        void Notify(string message);
    }
}