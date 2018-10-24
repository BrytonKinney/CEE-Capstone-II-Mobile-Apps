using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CapstoneApp.Shared.Entities
{
    [JsonObject]
    public class MirrorConfig
    {
        [JsonProperty(PropertyName = "Mirror")]
        public SmartMirror Mirror { get; set; }
        [JsonProperty(PropertyName = "Cfg")]
        public List<List<BaseEntity>> Configuration { get; set; }
    }
}
