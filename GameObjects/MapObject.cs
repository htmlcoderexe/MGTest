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
        public float spriteWidth = 16;
        public virtual void Tick()
        {
           
        }
        public void Tick(int Ticks)
        {
            for (int i = 0; i < Ticks; i++)
                this.Tick();
        }

        public void Render(SpriteBatch b, Texture2D t, Matrix m, float Scale)
        {
            Rectangle rekt = new Rectangle(0, 0, (int)spriteWidth, (int)spriteWidth);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m);
            //TODO!! Fix this so it properly gets tile index
            rekt.X = (int)(((float)this.Icon % spriteWidth) * spriteWidth);

            rekt.Y = (int)(((float)this.Icon / spriteWidth)) * (int)spriteWidth;
            b.Draw(t, new Vector2(this.X * spriteWidth*Scale, this.Y * spriteWidth*Scale), rekt, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

            b.End();
        }
    }
}
