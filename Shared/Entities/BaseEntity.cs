using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    public class BaseEntity
    {

        [SQLite.Column(Shared.Constants.DatabaseConstants.ID)]
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
    }
}
