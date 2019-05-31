using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;

namespace GameObjects.Items
{
    public class ItemMLGCan : Item
    {
        public ItemMLGCan()
        {
            this.Icon = 16;
            this.Name = "can of MLG";
            
        }
        public override bool Apply(Actor Source, Actor Target)
        {
            Player.Message("Faint airhorn sounds can be heard.");
            return false;
        }
    }
}
