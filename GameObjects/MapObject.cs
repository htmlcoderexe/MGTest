using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects
{
    public class MapObject
    {
        public Map ParentMap;
        public TimeSystem.Scheduler Command;
        public int X;
        public int Y;
        public int Icon;
        public bool IsDead { get; set; }
        public string Name="entity";
        public const float spriteWidth = 16;
        public virtual void Tick(float dT)
        {
           
        }
        public void Tick(int Ticks)
        {
            for (int i = 0; i < Ticks; i++)
                this.Tick(1.0f);
        }

        public virtual void Render(SpriteBatch b, Texture2D t, float Xoffset,float Yoffset, float Scale)
        {
            /*
            Rectangle rekt = new Rectangle(0, 0, (int)Map.spriteWidth, (int)Map.spriteWidth);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m);
            //TODO!! Fix this so it properly gets tile index
            rekt.X = (int)(((float)this.Icon % Map.spriteWidth) * Map.spriteWidth);

            rekt.Y = (int)(((float)this.Icon / Map.spriteWidth)) * (int)Map.spriteWidth;
            b.Draw(t, new Vector2(this.X * Map.spriteWidth*Scale, this.Y * Map.spriteWidth * Scale), rekt, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

            b.End();
            //*/

            
            Map.Renderer.RenderIconEx(b.GraphicsDevice, Xoffset+(float)this.X * (float)Map.spriteWidth * Scale, Yoffset+ (float)this.Y * (float)Map.spriteWidth * Scale, this.Icon, Scale);
        }
        public virtual void xRender(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer)
        {
//            X = (int)this.X * spriteWidth * Scale;
            Renderer.RenderIconEx(device, X, Y, this.Icon);
        }
    }
}
