using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;

namespace CapstoneApp.Shared.Entities
{
    [SQLite.Table(DatabaseConstants.Quadrant.QUAD_TABLE)]
    public class QuadrantSettings : BaseEntity
    {
        public QuadrantSettings() {}
        public QuadrantSettings(QuadrantSettingsModel model)
        {
            Quadrant = model.Quadrant;
            ItemType = model.ItemType;
        }

        [SQLite.Column(DatabaseConstants.Quadrant.QUADRANT)]
        public int Quadrant { get; set; }
        [SQLite.Column(DatabaseConstants.Quadrant.ITEM_TYPE)]
        public string ItemType { get; set; }
    }
}
