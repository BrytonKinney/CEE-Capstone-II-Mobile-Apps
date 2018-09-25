using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Constants
{
    /// <summary>
    /// Standard XML node types for RSS feeds
    /// </summary>
    public static class RssXmlConstants
    {
        /// <summary>
        /// News article parent node
        /// </summary>
        public const string ITEM = "item";
        
        /// <summary>
        /// Doubles as an article title node and the news feed name
        /// </summary>
        public const string TITLE = "title";
        public const string CHANNEL = "channel";
        public const string LINK = "link";
        public const string DESC = "description";
        public const string PUB_DATE = "pubDate";
    }

    /// <summary>
    /// The URLs for RSS feeds that will be listed by default
    /// </summary>
    public static class DefaultRssFeedUrls
    {
        #region Constant URLs

        /// <summary>
        /// Wall Street Journal Opinion RSS Feed
        /// </summary>
        public const string WsjOpinion = "http://www.wsj.com/xml/rss/3_7041.xml";

        /// <summary>
        /// New York Times Technology RSS Feed
        /// </summary>
        public const string NytTech = "http://rss.nytimes.com/services/xml/rss/nyt/Technology.xml";

        /// <summary>
        /// New York Times World News RSS Feed
        /// </summary>
        public const string NytWorld = "http://rss.nytimes.com/services/xml/rss/nyt/World.xml";

        /// <summary>
        /// Washington Post Politics RSS Feed
        /// </summary>
        public const string WaPoPolitics = "http://feeds.washingtonpost.com/rss/rss_election-2012";

        #endregion

        #region Public Methods

        public static List<string> GetAll()
        {
            return typeof(DefaultRssFeedUrls).GetFields(BindingFlags.Public | BindingFlags.Static |
                           BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(v => v.GetRawConstantValue().ToString()).ToList();
        }

        #endregion

    }
}
