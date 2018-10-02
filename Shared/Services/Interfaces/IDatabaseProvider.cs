using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Shared.Services.Interfaces
{
    public interface IDatabaseProvider
    {
        SQLiteAsyncConnection GetConnection();
        Task<int> AddOrUpdateAsync<T>(T item);
        Task<BaseEntity> GetAsync<BaseEntity>(int id) where BaseEntity : new();
    }
}
