using CapstoneApp.Shared.Entities;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CapstoneApp;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Entities.RssFeed;
using CapstoneApp.Shared.Services.Interfaces;
using LightInject;
using DBC = CapstoneApp.Shared.Constants.DatabaseConstants;
using System.Threading;

namespace Shared.Services.Implementations
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly SQLite.SQLiteAsyncConnection _connection;
        
        
        public DatabaseProvider()
        {
            _connection = new SQLiteAsyncConnection(DBC.DATABASE_FILE_LOCATION);
            Configure();
        }

        private async void Configure()
        {
            try
            {
                await _connection.CreateTablesAsync<RssFeed, WeatherLocations, SmartMirror, QuadrantSettings, GoogleEntity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _connection;
        }

        public async Task<int> AddOrUpdateAsync<T>(T item)
        {
            return await _connection.InsertOrReplaceAsync(item);
        }

        public async Task<T> GetAsync<T>(int id) where T : new()
        {
            return await _connection.GetAsync<T>(id);
        }

        public async Task<int> DeleteAsync<T>(T item)
        {
            return await _connection.DeleteAsync(item);
        }
    }
}
