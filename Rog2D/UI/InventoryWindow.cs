using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rog2D.UI
{
    public class InventoryWindow : GUI.Window
    {
        public GameObjects.Inventory Inventory;
        public InventoryWindow(GUI.WindowManager WM, GameObjects.Inventory Inventory)
        {
            this.Inventory = Inventory;
            Reload();
        }
        public void Reload()
        {
            this.Controls.Clear();
            InventoryControl c = new InventoryControl(WM, this.Inventory, 4);
            this.AddControl(c);
            this.Title = "Inventory";
            this.Width = c.Width;
            this.Height = c.Height;
        }
    }
}
