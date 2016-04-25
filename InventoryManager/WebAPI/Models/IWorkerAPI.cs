namespace WebAPI.Models
{
    public interface IWorkerAPI
    {
        void Add(string item, string authToken);
        string Retrieve(string s);
    }
}