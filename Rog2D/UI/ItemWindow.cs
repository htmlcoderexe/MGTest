using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rog2D.UI
{
    public class ItemWindow : GUI.Window
    {
        public GameObjects.Item Item;

        public ItemWindow(GUI.WindowManager WM, GameObjects.Item Item, bool ShowActions=true,bool PlayerView=true)
        {
            this.WM = WM;
            this.Item = Item;
            this.Width = 256 + this.Margin.X + this.Margin.Width;
            this.Height = 256;
            ItemSlot s = new ItemSlot(Item);
            s.CanGrab = false;
            s.CanPut = false;
            s.X = 80;
            GUI.Controls.RichTextDisplay t = new GUI.Controls.RichTextDisplay(256, 128, WM);
            t.SetText(Item.GetDescription());
            t.Y = 84;
            this.Title = Item.GetName();
            this.AddControl(t);
            this.AddControl(s);
            this.Center();
        }
    }
}
