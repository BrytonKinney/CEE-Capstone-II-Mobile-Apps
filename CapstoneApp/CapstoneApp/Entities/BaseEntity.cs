using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Constants;

namespace CapstoneApp.Shared.Entities
{
    public class BaseEntity
    {
        [SQLite.Column(DatabaseConstants.ID)]
        [SQLite.AutoIncrement, SQLite.PrimaryKey, SQLite.Indexed]
        public int Id { get; set; }
    }
}
