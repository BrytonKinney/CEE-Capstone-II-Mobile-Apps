using CapstoneApp.Models;
using CapstoneApp.Views;
using LightInject;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Entities.RssFeed;
using Xamarin.Forms;


namespace CapstoneApp.ViewModels
{
    public class RssFeedViewModel : BaseViewModel
    {
        public ObservableCollection<RssFeedModel> Items { get; set; }
        private CommandConstants.VIEWS currentView { get; set; }
        public Command LoadItemsCommand { get; set; }
        private IRssFeedReader _feedReader;
        private IDatabaseProvider _dbDriver;

        public RssFeedViewModel(CommandConstants.VIEWS command)
        {
            _dbDriver = App.Container.GetInstance<IDatabaseProvider>();
            if(command == CommandConstants.VIEWS.RssFeeds)
            {
                Title = ViewConstants.RSS_FEEDS_TITLE;
                currentView = command;
            }
            else
                Title = "None";
            Items = new ObservableCollection<RssFeedModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, RssFeedModel>(this, "AddRssFeed", async (obj, item) => await AddFeedAsync(obj, item));
            LoadItemsCommand.Execute(null);
        }

        async Task AddFeedAsync(NewItemPage obj, RssFeedModel item)
        {
            var newItem = item as RssFeedModel;
            RssFeed newFeed = new RssFeed(newItem.Url);
            await _feedReader.GetFeedArticles(newFeed).ContinueWith(async (t) =>
            {
                if(t.IsCanceled || t.IsFaulted)
                    Device.BeginInvokeOnMainThread(async () => await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "There was an issue adding the RSS feed.", "OK"));
                else
                {
                    await SaveEntity(newFeed);
                    Items.Add(new RssFeedModel(newFeed));
                }
            });
        }

        async Task LoadRSSFeeds()
        {
            _feedReader = App.Container.GetInstance<IRssFeedReader>();
            var db = _dbDriver;
            var rssFeeds = await db.GetConnection().Table<RssFeed>().ToListAsync();
            List<string> feedUrls = new List<string>();

            if (rssFeeds.Count == 0)
            {
                feedUrls = DefaultRssFeedUrls.GetAll().ToList();
                foreach (var feedUrl in feedUrls)
                {
                    RssFeed feed = new RssFeed(feedUrl);
                    await _feedReader.GetFeedArticles(feed).ContinueWith(async (t) =>
                    {
                        if (rssFeeds.All(rss => rss.FeedUrl != feedUrl))
                            await db.AddOrUpdateAsync(feed);
                        if (t.IsCompleted && !t.IsFaulted)
                            Items.Add(new RssFeedModel(feed));
                    });
                }
            }
            else
            {
                //feedUrls = rssFeeds.Select(rss => rss.FeedUrl).Distinct().ToList();
                foreach (var feed in rssFeeds)
                {
                    Items.Add(new RssFeedModel(feed));
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Items.Clear();
                await LoadRSSFeeds();
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