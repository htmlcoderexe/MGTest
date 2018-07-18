using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.MapObjects
{
    public class ItemDrop : MapObject
    {
        Item Item;
        public ItemDrop(Item Item)
        {
            this.Item = Item;
            this.Icon = this.Item.Icon;
        }
        
        public Item GetItem()
        {
            return this.Item;
        }
    }
}
