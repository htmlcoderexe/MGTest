using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.MapObjects
{
    public class Player : Actor, TimeSystem.IActor
    {
        public static Action<string> MessageCallback { get; set; }

        public bool IsActive;

        public float Readiness { get; set; }
        public float FillRate { get; set; }
        public Item Hotkey1Item { get; set; }
        public Action<Actor,Microsoft.Xna.Framework.Point> AimedAction;
        
        public Player() : base()
        {
            this.Icon = 1;
            this.FillRate = 1;
            this.StatBonuses.Add(new Mechanics.StatBonus() { Type = "PAtk", FlatValue = 3 });
        }

        public override void RequestMove(int X, int Y, Map Map, int speed)
        {
            if (!this.IsActive)
                return;
            this.IsActive = false;
            base.RequestMove(X, Y, Map, speed);
        }

        public override void RequestUseItem(Item Item)
        {
            if (!this.IsActive)
                return;
            this.IsActive = false;
            this.Readiness -= 0.75f;
            base.RequestUseItem(Item);
        }

        public static void Message(string s)
        {
            MessageCallback(s);
        }
        public override bool Move(int X, int Y, Map Map)
        {
           // return base.Move(X, Y, Map);
            bool f= base.Move(X, Y, Map);
            X = X + this.X;
            Y = Y + this.Y;
            if(!f)
            {
                MapObject bump = Map.ItemAt(X, Y);
                if (bump != null)
                {
                    if (bump is Monster m)
                    {
                        //#TODO: replace with actual damage/hit code
                        m.TakeDamage(this.CalculateBasicAttack());
                        if(m.Bars["HP"]<=0)
                        {
                            Player.Message("Player vanquishes " + m.Name + ".");
                            m.IsDead = true;
                        }
                    }
                    if (bump is ItemDrop d)
                    {
                        d.IsDead = true;
                        Item i = d.GetItem();
                        if (!i.Identified)
                        {
                            if (i is Items.ItemCard card)
                            {
                                card.Identified = Items.ItemCard.IdentifiedCards.Contains(card.Effect);
                            }
                        }

                        this.GiveItem(i);
                        this.X = d.X;
                        this.Y = d.Y;
                    }
                }
            }
            
            return f;
        }

        public void Act()
        {
            //this will tell the game to take player input
            this.IsActive = true;
        }

        public void GiveItem(Item Item)
        {
            Player.Message("You now have: "+Item.Name);
            this.Hotkey1Item = Item;
            this.Inventory.Prepare();
            this.Inventory.AddItem(Item);
            this.Inventory.Commit();
        }
    }
}
