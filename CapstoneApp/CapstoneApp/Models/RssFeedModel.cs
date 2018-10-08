using Shared.Entities.RssFeed;
using System;

namespace CapstoneApp.Models
{
    public class RssFeedModel
    {
        public RssFeedModel() {}
        public RssFeedModel(RssFeed feed)
        {
            Id = feed.Id.ToString();
            Text = feed.Name;
            Enabled = Convert.ToBoolean(feed.Enabled);
            Description = feed.Description;
            Url = feed.FeedUrl;
        }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Url { get; set; }
    }
}