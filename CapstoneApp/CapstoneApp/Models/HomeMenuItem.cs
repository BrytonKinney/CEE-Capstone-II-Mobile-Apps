using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneApp.Models
{
    public enum MenuItemType
    {
        Devices,
        RssFeeds,
        About,
        Weather,
        Google,
        Email
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
