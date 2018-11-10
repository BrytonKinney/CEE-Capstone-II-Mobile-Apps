using System;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Models
{
    public class GoogleDataModel
    {
        public GoogleDataModel(){}
        public GoogleDataModel(GoogleEntity entity){
            Email = entity.Email;
            AccessToken = entity.AccessToken;
            RefreshToken = entity.RefreshToken;
            ClientId = entity.RefreshToken;
        }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
    }
}











   

