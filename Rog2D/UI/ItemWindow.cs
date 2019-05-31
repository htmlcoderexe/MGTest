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
        InventoryWindow _parentWindow;
        public ItemWindow(GUI.WindowManager WM, GameObjects.Item Item,InventoryWindow ParentWindow, bool ShowActions=true,bool PlayerView=true)
        {
            this.WM = WM;
            this.Item = Item;
            this.Width = 256 + this.Margin.X + this.Margin.Width;
            this.Height = 256;
            this.Title = Item.Name;
            this._parentWindow = ParentWindow;
            ItemSlot s = new ItemSlot(Item)
            {
                CanGrab = false,
                CanPut = false,
                X = 80
            };
            GUI.Controls.RichTextDisplay t = new GUI.Controls.RichTextDisplay(256, 128, WM);
            t.SetText(Item.GetDescription());
            t.Y = 84;
            //this.Title = Item.GetName();
            this.AddControl(t);
            this.AddControl(s);
            GUI.Controls.Button b = new GUI.Controls.Button("Use")
            {
                Width = 128,
                Height = 32,
                Y = 84 + 128
            };
            b.Clicked += new GUI.Controls.Button.ClickHandler((object o, System.EventArgs e) => { this.Item.Apply(null); this._parentWindow.Reload(); this.Close(); });
            this.AddControl(b);
            b = new GUI.Controls.Button("Toss")
            {
                Width = 128,
                Height = 32,
                Y = 84 + 128,
                X = 128+4
            };
            b.Clicked += new GUI.Controls.Button.ClickHandler((object o, System.EventArgs e) => {
                //omfg you can actually just do this
                GameObjects.Item InventoryItem;
                for (int i=0;i< Scenes.MainGame.Player.Inventory.Items.Length;i++)
                {
                    InventoryItem = Scenes.MainGame.Player.Inventory.Items[i];
                    if (InventoryItem == Item)
                        Scenes.MainGame.Player.Inventory.Items[i] = null;
                }
                GameObjects.MapObjects.ItemDrop itemDrop = new GameObjects.MapObjects.ItemDrop(Item);
                itemDrop.X = Scenes.MainGame.Player.X;
                itemDrop.Y = Scenes.MainGame.Player.Y;
                itemDrop.ParentMap = Scenes.MainGame.Map;
                Scenes.MainGame.Map.AddObject(itemDrop);
                this._parentWindow.Reload();
                this.Close();
            });
            this.AddControl(b);
            this.Center();
        }
    }
}
