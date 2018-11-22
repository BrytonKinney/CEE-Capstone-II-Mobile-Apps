using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Models
{
    public class QuadrantSettingsModel
    {
        public QuadrantSettingsModel() {}
        public QuadrantSettingsModel(QuadrantSettings entity)
        {
            ItemType = entity.ItemType;
            Quadrant = entity.Quadrant;
        }

        public string ItemType { get; set; }
        
        public int Quadrant { get; set; }
    }
}
