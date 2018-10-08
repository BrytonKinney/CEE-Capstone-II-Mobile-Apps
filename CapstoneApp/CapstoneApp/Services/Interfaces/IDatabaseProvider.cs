using SQLite;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IDatabaseProvider
    {
        SQLiteAsyncConnection GetConnection();
        Task<int> AddOrUpdateAsync<T>(T item);
        Task<BaseEntity> GetAsync<BaseEntity>(int id) where BaseEntity : new();
    }
}
