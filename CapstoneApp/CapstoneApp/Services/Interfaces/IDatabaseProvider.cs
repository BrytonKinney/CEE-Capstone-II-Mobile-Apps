using SQLite;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IDatabaseProvider
    {
        SQLiteAsyncConnection GetConnection();
        Task<int> AddOrUpdateAsync<T>(T item);
        Task<T> GetAsync<T>(int id) where T : new();
    }
}
