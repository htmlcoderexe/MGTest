using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public override void Render(SpriteBatch b, Texture2D t, float Xoffset, float Yoffset, float Scale)
        {
            
            if (this.Item != null)
                this.Item.Render((int)(Xoffset+this.X * Map.spriteWidth * Scale), (int)(Yoffset+this.Y * Map.spriteWidth * Scale), b.GraphicsDevice, Map.Renderer, Scale);
//              base.Render(b, t, m, Scale);
        
        }
    }
}
