using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GUI
{
    public class Control
    {
        public Rectangle Bounds;
        public string Text;
        public List<Control> Children;

        public virtual void Render(SpriteBatch batch)
        {

        }
        public virtual void Update(GameTime time)
        {

        }
        public virtual bool TestHit(MouseState Mouse)
        {
            Point p = new Point(Mouse.X, Mouse.Y);
            return Bounds.Contains(p);
        }
        public virtual void MouseIn(MouseState Mouse)
        {

        }
        public virtual void MouseOut(MouseState Mouse)
        {

        }
        public virtual void MouseClick(MouseState Mouse)
        {

        }
    }
}
