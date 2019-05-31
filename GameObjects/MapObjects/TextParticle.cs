using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects.MapObjects
{
    public class TextParticle : MapObject
    {
        public float TimeLeft { get; set; }
        public Vector2 V0 {get;set;}
        public Vector2 dV { get; set; }
        public Vector2 Offset { get; set; }
        public Color Colour { get; set; }
        public string Text { get; set; }

        public override void Render(SpriteBatch b, Texture2D t, float Xoffset, float Yoffset, float Scale)
        {
            int rX = (int)(Offset.X+Xoffset + this.X * Map.spriteWidth * Scale);
            int rY = (int)(Offset.Y+Yoffset + this.Y * Map.spriteWidth * Scale);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            b.DrawString(Map.Renderer.UIFont, this.Text, new Vector2(rX, rY), this.Colour);
            b.End();
            //base.Render(b, t, Xoffset, Yoffset, Scale);
        }
        public override void Tick(float dT)
        {
            V0 += dV*dT;
            this.Offset += V0*dT;
            this.TimeLeft -= dT;
            if (this.TimeLeft <= 0)
                this.IsDead = true;
            base.Tick(dT);
        }
    }
}
