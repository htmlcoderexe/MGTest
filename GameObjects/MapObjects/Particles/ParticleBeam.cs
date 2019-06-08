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
        public Color Colour { get; set; }
        public float Width { get; set; }


        public override void Render(SpriteBatch b, Texture2D t, float Xoffset, float Yoffset, float Scale)
        {
            if (Width == 0)
                Width = 1f;
            int rX = (int)(Xoffset + this.X  * Map.spriteWidth * Scale);
            int rY = (int)(Yoffset + this.Y * Map.spriteWidth * Scale);
            Vector2 offset = new Vector2(0.5f, 0.5f)*Scale * Map.spriteWidth;
            float xDist = this.TargetX - this.X;
            float yDist = this.TargetY - this.Y;

            float angle = (float)Math.Atan2(yDist, xDist);
            float length = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
            Matrix rotator = Matrix.CreateRotationZ(angle);
            Vector2 offset2 = Vector2.Transform(new Vector2(0.5f,0f) * Map.spriteWidth*Scale, rotator);
            Vector2 scale = new Vector2(1f*length*2f, Width)*Scale;
            Rectangle source = new Rectangle(0, 0, 8, 8);
           // b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //draw stuff
            b.Draw(t, new Vector2(rX, rY)+offset+offset2, source, Colour, angle, new Vector2(0,4f), scale, SpriteEffects.None, 0f);
            //b.End();
        }
    }
}
