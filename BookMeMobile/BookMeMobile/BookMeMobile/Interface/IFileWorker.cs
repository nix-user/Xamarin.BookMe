using System.Threading.Tasks;

namespace BookMeMobile.Interface
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync();

        Task SaveTextAsync(string text);

        Task<string> LoadTextAsync();

        Task DeleteAsync();
    }
}