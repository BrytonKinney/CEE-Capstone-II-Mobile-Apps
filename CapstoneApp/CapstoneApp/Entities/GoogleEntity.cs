using System;
using Newtonsoft.Json;
using CapstoneApp.Shared.Constants;
using GA = CapstoneApp.Shared.Constants.JsonSerializerAttributes.GoogleAuth;
using CapstoneApp.Shared.Models;

namespace CapstoneApp.Shared.Entities
{
    [JsonObject]
    [SQLite.Table(DatabaseConstants.Google.GOOGLE_TABLE)]
    public class GoogleEntity : BaseEntity
    {
        public GoogleEntity() { }
        public GoogleEntity(GoogleDataModel model)
        {
            Id = 1; 
            AccessToken = model.AccessToken;
            RefreshToken = model.RefreshToken;
        }

        [JsonProperty(PropertyName = GA.AccessToken)]
        [SQLite.Column(DatabaseConstants.Google.ACCESS_TOKEN)]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = GA.RefreshToken)]
        [SQLite.Column(DatabaseConstants.Google.REFRESH_TOKEN)]
        public string RefreshToken { get; set; }

    }
}