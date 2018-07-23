using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;

namespace GameObjects
{
    public class Item 
    {
        public string Name { get; set; }
        public int UseTime { get; set; }
        public int Icon { get; set; }
        public bool Identified { get; set; }
        public int StackSize { get; set; }
        public Item()
        {
            this.UseTime = 25;
        }
        public virtual bool Apply(MapObjects.Actor Target)
        {
            Player.Message("This item does absolutely nothing.");
            return false;
        }

        public virtual bool Wear(Actor Target)
        {
            Player.Message("You can't wear that!");
            return false;
        }

        public virtual string GetDescription()
        {
            return "It doesn't look like anything in particular.";
        }

        public virtual string GetName()
        {
            return this.Name;
        }
    }
}
