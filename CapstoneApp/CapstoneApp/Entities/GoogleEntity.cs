using System;
using Newtonsoft.Json;
using CapstoneApp.Shared.Constants;
using GA = CapstoneApp.Shared.Constants.JsonSerializerAttributes.GoogleAuth;
using CapstoneApp.Shared.Models;

namespace CapstoneApp.Shared.Entities
{
    public class GoogleEntity : BaseEntity
    {
        public GoogleEntity(){}
        public GoogleEntity(GoogleDataModel model)
        {
            Email = model.Email;
            ClientId = model.ClientId;
            AccessToken = model.AccessToken;
            RefreshToken = model.RefreshToken;
            AuthUrl = AuthConstants.AuthorizeUrl;
            AccessTokenUrl = AuthConstants.AccessTokenUrl;
            Scope = AuthConstants.Scope;

        }

        [JsonProperty(PropertyName = GA.Email)]
        [SQLite.Column(DatabaseConstants.Google.EMAIL)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = GA.ClientId)]
        [SQLite.Column(DatabaseConstants.Google.CLIENT_ID)]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = GA.AccessToken)]
        [SQLite.Column(DatabaseConstants.Google.ACCESS_TOKEN)]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = GA.AccessTokenUrl)]
        [SQLite.Column(DatabaseConstants.Google.ACCESS_TOKEN_URL)]
        public string AccessTokenUrl { get; set; }

        [JsonProperty(PropertyName = GA.RefreshToken)]
        [SQLite.Column(DatabaseConstants.Google.REFRESH_TOKEN)]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = GA.AuthUrl)]
        [SQLite.Column(DatabaseConstants.Google.AUTH_URL)]
        public string AuthUrl { get; set; }

        //Add storing scopes in DB 
        [JsonProperty(PropertyName = GA.Scope)]
        [SQLite.Column(DatabaseConstants.Google.SCOPE)]
        public string Scope { get; set; }


    }
}