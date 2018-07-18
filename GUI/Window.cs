using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class Window :Control
    {

        public override void Render(SpriteBatch batch)
        {
            GUI.Drawing.DrawFrame(this.Bounds, batch, Drawing.WindowSkin);
            GUI.Drawing.DrawString(new Microsoft.Xna.Framework.Vector2(this.Bounds.X+3, this.Bounds.Y+3), batch, Drawing.DefaultFont, this.Text, Microsoft.Xna.Framework.Color.White);
            base.Render(batch);
        }
    }
}
