using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CapstoneApp.Shared.Entities
{
    [JsonObject]
    [SQLite.Table(DatabaseConstants.Mirror.MIRROR_TABLE)]
    public class SmartMirror : BaseEntity
    {
        public SmartMirror() {}

        public SmartMirror(SmartMirrorModel model)
        {
            IpAddress = model.IpAddressString;
            HostName = model.HostName;
        }

        //[SQLite.Column(Constants.DatabaseConstants.ID)]
        //[SQLite.PrimaryKey, SQLite.AutoIncrement]
        //public int Id { get; set; }

        [JsonProperty(PropertyName = Constants.JsonSerializerAttributes.MirrorConfiguration.IpAddress)]
        [SQLite.Column(Constants.DatabaseConstants.Mirror.IP_ADDR)]
        public string IpAddress { get; set; }

        [JsonProperty(PropertyName = Constants.JsonSerializerAttributes.MirrorConfiguration.HostName)]
        [SQLite.Column(Constants.DatabaseConstants.Mirror.HOSTNAME)]
        public string HostName { get; set; }
    }
}
