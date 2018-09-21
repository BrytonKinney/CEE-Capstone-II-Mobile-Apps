using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.RssFeed
{
    public class RssFeed
    {
        private string _rssUrl { get; set; }

        private string _feedName { get; set; }

        private string _description { get; set; }

        private List<Article> _articles { get; set; }

        #region Constructor(s)

        /// <summary>
        /// Constructor for an RSS Feed
        /// </summary>
        /// <param name="rssFeedUrl"></param>
        public RssFeed(string rssFeedUrl)
        {
            _rssUrl = rssFeedUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A read-only property to access the feed's url.
        /// </summary>
        public string FeedUrl => _rssUrl;

        /// <summary>
        /// Read-only property for the name of the feed.
        /// </summary>
        public string Name => _feedName;

        /// <summary>
        /// Read-only field describing the feed.
        /// </summary>
        public string Description => _description;

        public List<Article> Articles => _articles;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called after the feed is read initially to populate the basic feed information.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void SetFeedInformation(string name, string description, List<Article> articles)
        {
            _feedName = name;
            _description = description;
            _articles = articles;
        }

        #endregion
    }
}
