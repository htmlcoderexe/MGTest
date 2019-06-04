using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameObjects.MapObjects.Particles
{
    public class ParticleBeam : Particle
    {
        public int TargetX { get; set; }
        public int TargetY { get; set; }


        public override void Render(SpriteBatch b, Texture2D t, float Xoffset, float Yoffset, float Scale)
        {
            int rX = (int)(Xoffset + this.X  * Map.spriteWidth * Scale);
            int rY = (int)(Yoffset + this.Y * Map.spriteWidth * Scale);

            float xDist = this.TargetX - this.X;
            float yDist = this.TargetY - this.Y;

            float angle = (float)Math.Atan2(yDist, xDist);
            float length = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
            Vector2 scale = new Vector2(1f*length*2f, 1f)*Scale;
            Rectangle source = new Rectangle(0, 0, 8, 8);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //draw stuff
            b.Draw(t, new Vector2(rX, rY), source, Color.Lime, angle, Vector2.Zero, scale, SpriteEffects.None, 0f);
            b.End();
        }
    }
}
