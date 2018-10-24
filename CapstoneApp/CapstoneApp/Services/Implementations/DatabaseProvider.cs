using CapstoneApp.Shared.Entities;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneApp;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Services.Interfaces;
using LightInject;
using DBC = CapstoneApp.Shared.Constants.DatabaseConstants;
namespace Shared.Services.Implementations
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly SQLite.SQLiteAsyncConnection _connection;
        private IEventHandler _handler;
        private event EventHandler<ConfigurationEventArgs> SettingsChanged;
        public DatabaseProvider()
        {
            _connection = new SQLiteAsyncConnection(DBC.DATABASE_FILE_LOCATION);
            _handler = App.Container.GetInstance<IEventHandler>();
            Configure();
            SettingsChanged = _handler.CaptureEvent;
        }

        private async void Configure()
        {
            try
            {
                await _connection.CreateTablesAsync<RssFeed, WeatherLocations, SmartMirror>();
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

        protected virtual void OnSettingsChanged()
        {
            if (SettingsChanged != null)
            {
                var mirror = _connection.GetConnection().Table<SmartMirror>().First();
                var feeds = _connection.GetConnection().Table<RssFeed>();
                var weatherLocations = _connection.GetConnection().Table<WeatherLocations>();
                List<List<BaseEntity>> config = new List<List<BaseEntity>>();
                // Definitely not the most performant solution
                config.Add(feeds.ToList().Cast<BaseEntity>().ToList());
                config.Add(weatherLocations.ToList().Cast<BaseEntity>().ToList());
                SettingsChanged(this, new ConfigurationEventArgs(new MirrorConfig { Mirror = mirror, Configuration = config }));
            }
        }
    }
}
