﻿using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CapstoneApp.Shared.Entities.RssFeed;

namespace Shared.Services.Implementations
{
    public class RssFeedReader : IRssFeedReader
    {
        private readonly IXmlRssFeedParser _xmlReader;

        public RssFeedReader() {}

        public RssFeedReader(IXmlRssFeedParser xmlReader)
        {
            _xmlReader = xmlReader;
        }

        public async Task<List<Article>> GetFeedArticles(RssFeed feed)
        {
            WebClient client = new WebClient();
            byte[] resp = await client.DownloadDataTaskAsync(new Uri(feed.FeedUrl));
            _xmlReader.ParseFeed(feed, resp);
            return feed.Articles;
        }
    }
}
