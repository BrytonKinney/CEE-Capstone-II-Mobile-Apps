using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LightInject;
using Shared.Constants;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;

namespace Android_Capstone
{
    [Activity(Label = "RssFeeds")]
    public class RssFeeds : ListActivity
    {
        private readonly IRssFeedReader _feedReader;
        private List<RssFeed> _rssFeeds = new List<RssFeed>();

        public RssFeeds()
        {
            if (_feedReader == null)
                _feedReader = MainActivity.Container.GetInstance<IRssFeedReader>();
        }

        public RssFeeds(IRssFeedReader feedReader)
        {
            _feedReader = feedReader;
        }

        public async Task GatherRssFeeds()
        {
            foreach (var feed in DefaultRssFeedUrls.GetAll())
            {
                RssFeed rssFeed = new RssFeed(feed);
                await _feedReader.GetFeedArticles(rssFeed);
                _rssFeeds.Add(rssFeed);
            }
            RunOnUiThread(() =>
            {
                ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.rss_feeds, _rssFeeds.Select(x => x.Name).ToList());
            });
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Task.Run(async () => await GatherRssFeeds());
            // Create your application here
        }
    }
}