﻿using System;
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
    [Activity(Label = "Rss Feeds")]
    public class RssFeeds : ListActivity
    {
        private readonly IRssFeedReader _feedReader;
        private readonly IDatabaseProvider _provider;
        private List<RssFeed> _rssFeeds = new List<RssFeed>();

        public RssFeeds()
        {
            if (_feedReader == null)
                _feedReader = MainActivity.Container.GetInstance<IRssFeedReader>();
            if (_provider == null)
                _provider = MainActivity.Container.GetInstance<IDatabaseProvider>();
        }

        public RssFeeds(IRssFeedReader feedReader)
        {
            _feedReader = feedReader;
        }

        public async Task GatherRssFeeds()
        {
            var feeds = await _provider.GetConnection().Table<RssFeed>().ToListAsync();
            if (!feeds.Any())
            {
                foreach (var feed in DefaultRssFeedUrls.GetAll())
                {
                    RssFeed rssFeed = new RssFeed(feed);
                    await _feedReader.GetFeedArticles(rssFeed);
                    _rssFeeds.Add(rssFeed);
                }

                await _provider.GetConnection().InsertAllAsync(_rssFeeds);
            }
            RunOnUiThread(() =>
            {
                ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.rss_feeds, feeds.Select(x => x.Name).ToList());
                var listView = FindViewById<ListView>(Resource.Layout.rss_feeds);
                this.ListView.ItemClick += (s, args) =>
                {
                    Intent intent = new Intent(this, typeof(RssFeedDetails));
                    intent.PutExtra("Feed", Newtonsoft.Json.JsonConvert.SerializeObject(feeds.ElementAt(args.Position)));
                    StartActivity(intent);
                };
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