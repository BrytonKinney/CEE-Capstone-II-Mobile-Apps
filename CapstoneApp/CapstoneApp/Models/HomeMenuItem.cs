using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneApp.Models
{
    public enum MenuItemType
    {
        RssFeeds,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
