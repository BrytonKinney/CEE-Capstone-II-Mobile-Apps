using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Constants;
using Newtonsoft.Json;
using Shared.Entities.RssFeed;

namespace CapstoneApp.Shared.Entities
{
    [JsonObject]
    public class MirrorConfig
    {
        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.Mirror)]
        public SmartMirror Mirror { get; set; }
        
        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.Config)]
        public List<QuadrantSettings> Configuration { get; set; }

        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.RssFeeds)]
        public List<RssFeed.RssFeed> RssFeeds { get; set; }

        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.WeatherLocations)]
        public List<WeatherLocations> WeatherLocations { get; set; }

        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.QuadrantSettings)]
        public List<QuadrantSettings> Quadrants { get; set; }

        [JsonProperty(PropertyName = JsonSerializerAttributes.MirrorConfiguration.GoogleSettings)]
        public List<GoogleEntity> GoogleInfo { get; set; }
    }
}
