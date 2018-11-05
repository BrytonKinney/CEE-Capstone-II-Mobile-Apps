using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Auth;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Constants
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
    /// Holds constants for Newtonsoft's JSON
    /// </summary>
    public static class JsonSerializerAttributes
    {
        public static class RssFeeds
        {
            public const string FeedName = "Name";
            public const string FeedUrl = "FeedUrl";
            public const string Enabled = "Enabled";
            public const string Description = "Description";
            public const string Articles = "Articles";
            public const string MaxArticles = "MaxArticles";
        }


        public static class MirrorConfiguration
        {
            public const string Mirror = "Mirror";
            public const string Config = "Cfg";
            public const string HostName = "HostName";
            public const string IpAddress = "IpAddress";
            public const string RssFeeds = "RssFeeds";
            public const string WeatherLocations = "WeatherLocations";


            public static class GoogleAuth
            {
                public const string Email = "Email";
                public const string AccessToken = "AccessToken";
                public const string AccessTokenUrl = "AccessTokenUrl";
                public const string RefreshToken = "RefreshToken";
                public const string AuthUrl = "AuthUrl";
                public const string ClientId = "ClientId";
                public const string Scope = "Scope";

            }
        }

        public static class DefaultQuadrantSettings
        {
            public static QuadrantSettings[] Defaults =
            {
            new QuadrantSettings()
            {
                ItemType = QuadrantConstants.ItemTypes.RSS_FEEDS, Quadrant = QuadrantConstants.Q1
            },
            new QuadrantSettings()
            {
                ItemType = QuadrantConstants.ItemTypes.WEATHER_LOCATIONS, Quadrant = QuadrantConstants.Q2
            }
        };
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
        public static class WeatherSettings
        {
            public enum Location
            {
                ZIP,
                Coordinates,
                CityCountry
            }
            public static string[] LocationDropdownOptions = { "ZIP Code", "Coordinates (Your Current Location)", "City and Country Code" };
        }
        public static class DefaultWeatherServices
        {
            // public static WeatherModel OpenWeather
        }

        /// <summary>
        /// Constants like lengths, table names, etc.
        /// </summary>
        public static class DatabaseConstants
        {
            public const string DATABASE_FILE_NAME = "capstone_app_settings.db";
            public static string DATABASE_FILE_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_FILE_NAME);
            public const string ID = "id";

            public static class Mirror
            {
                public const string MIRROR_TABLE = "mirror";
                public const string IP_ADDR = "ipaddress";
                public const string IS_SELECTED = "isselected";
                public const string HOSTNAME = "hostname";
            }

            public static class Quadrant
            {
                public const string ITEM_TYPE = "itemtype";
                public const string QUADRANT = "quadrant";
                public const string QUAD_TABLE = "quadrants";
            }
            public static class RSS
            {
                public const string RSS_FEED_TABLE = "rssfeeds";
                public const string RSS_FEED_ID = "id";
                public const string RSS_FEED_URL = "url";
                public const string RSS_FEED_NAME = "name";
                public const string RSS_FEED_DESCRIPTION = "description";
                public const string RSS_FEED_ENABLED = "enabled";
                public const string RSS_FEED_MAX_ARTICLES = "articlenum";
            }
            public static class WeatherLocations
            {
                public const string WEATHER_LOCATION_TABLE = "weatherlocations";
                public const string WEATHER_FEED_ID = "id";
                public const string WEATHER_NAME = "name";
                public const string WEATHER_ENABLED = "enabled";
                public const string WEATHER_LOCATION_STRING = "locationstring";
                public const string WEATHER_LOCATION_CODE = "locationcode";
            }

            public static class Google
            {
                public const string GOOGLE_TABLE = "googletable";
                public const string GOOGLE_ID = "id";
                public const string EMAIL = "email";
                public const string ACCESS_TOKEN = "accesstoken";
                public const string ACCESS_TOKEN_URL = "accesstokenurl";
                public const string REFRESH_TOKEN = "refreshtoken";
                public const string AUTH_URL = "authurl";
                public const string CLIENT_ID = "clientid";
                public const string SCOPE = "scope";
            }


        }

        public static class ViewConstants
        {
            public const string RSS_FEEDS_TITLE = "RSS Feeds";
            public const string NEW_WEATHER_LOCATION = "Add a New Weather Location";
        }

        public static class CommandConstants
        {
            public enum VIEWS
            {
                RssFeeds
            };
        }


        public static class QuadrantConstants
        {
            public static class ItemTypes
            {
                public const string RSS_FEEDS = "rss";
                public const string WEATHER_LOCATIONS = "weather";
                public const string EMAIL = "email";
            }

            public const int Q1 = 1;
            public const int Q2 = 2;
            public const int Q3 = 3;
            public const int Q4 = 4;
            public const int Q5 = 5;
        }

        public static class AuthConstants
        {
            public static string iOSClientId = "178940052019-4ba9dcg9r991jipls85ag0c1j53lnbo2.apps.googleusercontent.com";
            public static string AndroidClientId = "<insert Android client ID here>";

            // These values do not need changing
            public static string Scope = "https://www.googleapis.com/auth/plus.me https://mail.google.com/"; //https://mail.google.com/  https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile
            public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
            public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
            public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

            // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
            public static string iOSRedirectUrl = "com.googleusercontent.apps.178940052019-4ba9dcg9r991jipls85ag0c1j53lnbo2:/oauth2redirect";
            public static string AndroidRedirectUrl = "<insert Android redirect URL here>:/oauth2redirect";

        }

        public class AuthenticationState
        {
            public static OAuth2Authenticator Authenticator;
        }
    }
}
