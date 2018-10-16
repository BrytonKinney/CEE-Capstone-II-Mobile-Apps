using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using DBC = Shared.Constants.DatabaseConstants;
using JC = Shared.Constants.JsonSerializerAttributes.RssFeeds;
using RC = Shared.Constants.DatabaseConstants.RSS;
namespace Shared.Entities.RssFeed
{
    [Table(RC.RSS_FEED_TABLE)]
    public class RssFeed
    {
        #region Constructor(s)

        public RssFeed() {}

        /// <summary>
        /// Constructor for an RSS Feed
        /// </summary>
        /// <param name="rssFeedUrl"></param>
        public RssFeed(string rssFeedUrl)
        {
            FeedUrl = rssFeedUrl;
        }

        #endregion

        #region Properties

        [Column(DBC.ID)]
        [PrimaryKey, AutoIncrement, Indexed]
        public int? Id { get; set; }

        /// <summary>
        /// A read-only property to access the feed's url.
        /// </summary>
        [JsonProperty(PropertyName = JC.FeedUrl)]
        [SQLite.Column(RC.RSS_FEED_URL)]
        public string FeedUrl { get; set; }

        /// <summary>
        /// Read-only property for the name of the feed.
        /// </summary>
        [JsonProperty(PropertyName = JC.FeedName)]
        [SQLite.Column(RC.RSS_FEED_NAME)]
        public string Name { get; set; }

        /// <summary>
        /// Read-only field describing the feed.
        /// </summary>
        [JsonProperty(PropertyName = JC.Description)]
        [SQLite.Column(RC.RSS_FEED_DESCRIPTION)]
        public string Description {get; set; }

        [SQLite.Ignore()]
        [JsonProperty(PropertyName = JC.Articles)]
        public List<Article> Articles { get; set; }

        [JsonProperty(PropertyName = JC.Enabled)]
        [SQLite.Column(RC.RSS_FEED_ENABLED)]
        public int Enabled { get; set; }

        [JsonProperty(PropertyName = JC.MaxArticles)]
        [SQLite.Column(RC.RSS_FEED_MAX_ARTICLES)]
        public int MaxArticles { get; set; }

        #endregion

    }
}
