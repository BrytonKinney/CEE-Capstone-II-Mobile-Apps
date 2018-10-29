using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Constants;

namespace CapstoneApp.Shared.Entities
{
    [SQLite.Table(DatabaseConstants.Quadrant.QUAD_TABLE)]
    public class QuadrantSettings : BaseEntity
    {
        [SQLite.Column(DatabaseConstants.Quadrant.QUADRANT)]
        public int Quadrant { get; set; }
        [SQLite.Column(DatabaseConstants.Quadrant.ITEM_TYPE)]
        public string ItemType { get; set; }
    }
}
