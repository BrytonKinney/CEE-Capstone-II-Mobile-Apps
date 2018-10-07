﻿using Shared.Entities.RssFeed;
using System;

namespace Android_CapstoneApp.Models
{
    public class Item
    {
        public Item() {}
        public Item(RssFeed feed)
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