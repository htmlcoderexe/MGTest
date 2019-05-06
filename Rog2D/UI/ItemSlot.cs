using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rog2D.UI
{
    public class ItemSlot : GUI.Control
    {
        public GameObjects.Item Item;
        public bool CanGrab;
        public bool CanPut;
        public delegate void ItemEventHandler(object sender, ItemEventArgs e);
        public class ItemEventArgs : System.ComponentModel.CancelEventArgs
        {
            public GameObjects.Item Item;
            public ItemEventArgs(GameObjects.Item Item)
            {
                this.Item = Item;
            }
        }
        public event ItemEventHandler ItemOut;
        public event ItemEventHandler ItemIn;
        public event ItemEventHandler BeforeItemChanged;
        public event ItemEventHandler ItemInspected;
        public ItemSlot(GameObjects.Item Item)
        {
            this.Item = Item;
            this.Width = 80;
            this.Height = 80;
            this.CanGrab = true;
        }
        public override void Render(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, GUI.Renderer Renderer, int X, int Y)
        {
            X += this.X;
            Y += this.Y;
            Renderer.SetTexture(Renderer.WindowSkin);
            GUI.Renderer.Rect r = new GUI.Renderer.Rect(48, 48, 40, 40);
            Renderer.RenderQuad(device, X, Y, Width, Height, r);
            Renderer.SetTexture(Assets.SpriteSheets["sprites1"]);
            if (Item != null)
                Item.Render(X + 8, Y + 8, device, Renderer,4);
            base.Render(device, Renderer, X, Y);
        }
        public override void Click(float X, float Y)  //bad bad bad bad bad!! this is an overrideable method instead of the intended event.
        {
            if (!CanGrab && !CanPut) //nothing to do here
            {
                if (this.Item != null)
                {
                    ItemInspected?.Invoke(this, new ItemEventArgs(this.Item));
                }
                return;
            }
            /*
            GameObjects.Item mouseItem = WM.MouseGrab;
            GameObjects.Item currentItem = this.Item;
            if (mouseItem == null)
            {
                if (currentItem == null) //nothing to do here
                {
                    return;
                }
                else // take the item
                {
                    WM.MouseGrab = currentItem;
                    ItemOut?.Invoke(this, new ItemEventArgs(currentItem));
                    this.Item = null;
                }
            }
            else //if mouse has something
            {
                if (!CanPut) //can't grab item: mouse full
                    return;
                ItemEventArgs iargs = new ItemEventArgs(mouseItem);
                BeforeItemChanged?.Invoke(this, iargs);
                if (iargs.Cancel) //custom function declined the item or no custom function
                    return;
                if (currentItem == null) //put in item
                {
                    this.Item = mouseItem;
                    ItemIn?.Invoke(this, new ItemEventArgs(mouseItem));
                    WM.MouseGrab = null;

                }
                else //swap items
                {
                    this.Item = mouseItem;
                    WM.MouseGrab = currentItem;
                    ItemOut?.Invoke(this, new ItemEventArgs(currentItem));
                    ItemIn?.Invoke(this, new ItemEventArgs(mouseItem));

                }
            }
            */
            base.Click(X, Y);
        }
        public override void MouseMove(float X, float Y)
        {
            if (WM.MouseStillSeconds > 0.5f && this.Item != null)
            {
             //   ToolTipWindow tip = new ToolTipWindow(this.WM, this.Item.GetTooltip(), WM.MouseX, WM.MouseY, true);
               // WM.Add(tip);
            }
        }
    }
}
