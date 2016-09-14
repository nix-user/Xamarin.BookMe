using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMeMobile.Interface
{
    public interface IFileWork
    {
        Task<bool> ExistsAsync();

        Task SaveTextAsync(string text);

        Task<string> LoadTextAsync();

        Task DeleteAsync();
    }
}