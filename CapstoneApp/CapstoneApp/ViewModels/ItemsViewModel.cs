using CapstoneApp.Models;
using CapstoneApp.Views;
using LightInject;
using Shared.Constants;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CapstoneApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        private CommandConstants.VIEWS currentView { get; set; }
        public Command LoadItemsCommand { get; set; }
        private IRssFeedReader _feedReader;

        public ItemsViewModel(CommandConstants.VIEWS command)
        {
            if(command == CommandConstants.VIEWS.RssFeeds)
            {
                Title = ViewConstants.RSS_FEEDS;
                currentView = command;
            }
            else
                Title = "None";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                RssFeed newFeed = new RssFeed(newItem.Url);
                await _feedReader.GetFeedArticles(newFeed).ContinueWith(async (t) =>
                {
                    if(t.IsCanceled || t.IsFaulted)
                        Device.BeginInvokeOnMainThread(async () => await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "There was an issue adding the RSS feed.", "OK")); //App.Container.GetInstance<IMessageDisplay>().LongAlert("Failed to add RSS Feed.");
                    else
                    {
                        var dbDriver = App.Container.GetInstance<IDatabaseProvider>();
                        await dbDriver.AddOrUpdateAsync(newFeed);
                        Items.Add(new Item(newFeed));
                    }
                });
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                _feedReader = App.Container.GetInstance<IRssFeedReader>();
                var db = App.Container.GetInstance<IDatabaseProvider>().GetConnection();
                var rssFeeds = await db.Table<RssFeed>().ToListAsync();
                List<string> rssFeedUrls = DefaultRssFeedUrls.GetAll();
                if(rssFeeds.Count > 0)
                    rssFeedUrls.AddRange(rssFeeds.Select(rss => rss.FeedUrl).ToList());
                foreach(var feedUrl in rssFeedUrls)
                {
                    RssFeed feed = new RssFeed(feedUrl);
                    await _feedReader.GetFeedArticles(feed).ContinueWith(async (t) =>
                    {
                        await db.InsertOrReplaceAsync(feed);
                        if(t.IsCompleted && !t.IsFaulted)
                            Items.Add(new Item(feed));
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}