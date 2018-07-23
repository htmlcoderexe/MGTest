using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rog2D.UI
{
    public class InventoryWindow : GUI.Window
    {
//        public GameObjects.Inventory Inventory;
        public InventoryWindow(GUI.WindowManager WM, GameObjects.Inventory Inventory)
        {
            InventoryControl c = new InventoryControl( WM, Inventory,4);
            this.Controls.Add(c);
            this.Title = "Inventory";
            this.Width = 256;
            this.Height = 256;
        }
    }
}
