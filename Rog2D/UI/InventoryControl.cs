using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework.Graphics;

namespace Rog2D.UI
{
    public class IconItem : GUI.IActionIcon
    {
        public float CoolDown { get; set; }

        public int Icon { get; set; }

        public float MaxCoolDown { get; set; }

        public string Name { get; set; }

        public int StackSize { get; set; }

        Item Item;

        public IconItem(Item Item)
        {
            if (Item == null)
                return;
            this.Icon = Item.Icon;
            this.Name = Item.GetName();
            this.StackSize = Item.StackSize;
            this.Item = Item;
        }

        public Item GetItem()
        {
            return this.Item;
        }

        public List<string> GetTooltip()
        {
            throw new NotImplementedException();
        }

        public void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetTexture(Assets.SpriteSheets["sprites1"]);
            GameObjects.Items.ItemWand w = this.Item as GameObjects.Items.ItemWand;
            if (w != null)
            {
                Renderer.SetColour(w.Component1Color);
                Renderer.RenderIconEx(device, X, Y, w.Component1 + 64);
                Renderer.SetColour(w.Component2Color);
                Renderer.RenderIconEx(device, X, Y, w.Component2 + 80);
                Renderer.SetColour(w.Component3Color);
                Renderer.RenderIconEx(device, X, Y, w.Component3 + 96);
            }
            else
            {
                Renderer.RenderIconEx(device, X, Y, this.Icon);
            }
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }

    public class InventoryControl : GUI.Control
    {
        public Inventory Inventory;

        public InventoryControl(GUI.WindowManager WM, Inventory Inventory, int Width = 8)
        {
            int slotwidth = 80;
            this.Width = Width * slotwidth;
            int Rows = (int)Math.Ceiling((float)Inventory.Items.Length / (float)Width);
            this.Height = Rows * slotwidth;
            for (int y = 0; y < Rows; y++)
                for (int x = 0; x < Width; x++)
                {
                    int i = y * Width + x;
                    Item item = Inventory.Items[i];
                    ItemSlot s = new ItemSlot(item);
                    s.X = x * slotwidth;
                    s.Y = y * slotwidth;
                    //*
                    s.CanGrab = false;
                    s.CanPut = false;

                    //*/
                    //s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => { if(((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item) ==null) e.Cancel=true; });
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = null; });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = (e as ItemSlot.ItemEventArgs).Item; });
                    this.AddControl(s);
                }
        }


    }
}
