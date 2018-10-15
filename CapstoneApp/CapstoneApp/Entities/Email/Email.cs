using System;
using CapstoneApp.Shared.Models;
using Shared.Constants;
using Shared.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapstoneApp.Shared.Entities
{

    [SQLite.Table(DatabaseConstants.Email.EMAIL_TABLE)]
    public class Email : BaseEntity
    {
        public Email() { }
        public Email(EmailModel model)
        {
            model.EmailAddress = EmailAddress;
            model.Password = Password;
        }

        [SQLite.Column(DatabaseConstants.Email.EMAIL_ADDRESS)]
        public string EmailAddress { get; set; }
        [SQLite.Column(DatabaseConstants.Email.EMAIL_PASSWORD)]
        public string Password { get; set; }
    }
}
