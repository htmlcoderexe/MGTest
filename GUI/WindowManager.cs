using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class WindowManager
    {
        public List<Window> Windows;

        public void Render(SpriteBatch batch)
        {
            foreach(Window w in this.Windows)
            {
                w.Render(batch);
            }
        }
        public void Top(Window Window)
        {
            Window tmp;
            int idxc = this.Windows.LastIndexOf(Window);
            tmp = this.Windows[idxc];
            for (int i = idxc; i < this.Windows.Count - 1; i++)
            {
                this.Windows[i] = this.Windows[i + 1];
            }
            this.Windows[this.Windows.Count - 1] = Window;
        }
    }
}
