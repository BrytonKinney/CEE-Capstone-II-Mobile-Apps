using System;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Models
{
    public class GoogleDataModel
    {
        public GoogleDataModel(){}
        public GoogleDataModel(GoogleEntity entity){

            AccessToken = entity.AccessToken;
            RefreshToken = entity.RefreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}











   

