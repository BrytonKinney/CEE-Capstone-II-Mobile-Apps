using System;
using CapstoneApp.Shared.Entities;
namespace CapstoneApp.Shared.Models
{
    public class EmailModel
    {
        public EmailModel()
        {
        }

        public EmailModel(Email item){
            item.EmailAddress = EmailAddress;
            item.Password = Password;
        }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
