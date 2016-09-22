using System.Threading.Tasks;

namespace BookMeMobile.Interface
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string fileName);

        Task SaveTextAsync(string fileName, string text);

        Task<string> LoadTextAsync(string fileName);

        Task DeleteAsync(string fileName);
    }
}