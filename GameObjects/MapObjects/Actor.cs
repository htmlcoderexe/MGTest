using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.Mechanics;

namespace GameObjects.MapObjects
{
    public class Actor : MapObject
    {
        public enum FriendlinessValue
        {
            Neutral,
            Peaceful,
            Hostile,
            Tame
        }
        public Actor Target;
        public FriendlinessValue Friendliness { get; set; }
        public bool CanPhase { get; set; }
        public Inventory Inventory { get; set; }
        public List<StatBonus> StatBonuses { get; private set; }
        public Dictionary<string, float> Bars;
        public int Speed;

        

        public float CalculateBasicAttack()
        {
            return 1 + this.CalculateStat("PAtk");
        }

        //Wrapping the static to use a single param and attach it to the actor instead
        public float CalculateStat(string statname)
        {
            return StatBonus.CalculateStat(statname, this.StatBonuses);
        }

        public void TakeDamage(float Damage)
        {
            //#TODO: spawn damage particle
            TextParticle p = new TextParticle() { Text = Damage.ToString(), Colour = Color.Red, TimeLeft = 0.5f, V0 = new Vector2(0, -16f),X=this.X,Y=this.Y,Offset=new Vector2(0,-32f) };
            this.ParentMap.Particles.Add(p);
            ParentMap.AnimationActionFreeze += 0.5f;
            this.Bars["HP"] -= Damage;
        }

        public Actor()
        {
            this.Inventory = new Inventory(16);
            this.StatBonuses = new List<StatBonus>();
            this.Bars = new Dictionary<string, float>();
            this.StatBonuses.Add(new StatBonus() {Type="HPMax", FlatValue=20,Order=StatBonus.StatOrder.Template });
            this.Bars["HP"] = CalculateStat("HPMax");
            this.Bars["MP"] = 0;
        }

        public Point GetNextStep(int X, int Y, Map Map)
        {
            //for X and Y check first if it's zero, otherwise normalize to +/-1
            X = (X - this.X) == 0 ? 0 : (X - this.X) / (Math.Abs(X - this.X));
            Y = (Y - this.Y) == 0 ? 0 : (Y - this.Y) / (Math.Abs(Y - this.Y));
            return new Point(X, Y);
        }
        public bool StepTo(int X, int Y, Map Map)
        {
            Point p = GetNextStep(X, Y, Map);
            return Move(p.X, p.Y, Map);

        }
        // all commands go here
        public virtual void RequestUseItem(Item Item)
        {
            Item.Apply(this,null);
            //
            return;
            GameObjects.Actions.UseItem u = new GameObjects.Actions.UseItem();
            u.Time = Item.UseTime;
            u.Item = Item;
            u.Owner = this as TimeSystem.IActor;
            this.Command.Add(u);
        }
        public virtual void RequestMove(int X, int Y, Map Map, int speed)
        {

            //
            Move(X, Y, Map);
            return;
            Actions.MoveStep m = new Actions.MoveStep();
            m.X = X;
            m.Y = Y;
            m.Map = this.ParentMap;
            m.Time = speed;
            m.Owner = this as TimeSystem.IActor;
            Command.Add(m);
        }
        /// <summary>
        /// Attempts to move towards indicated offset
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Map"></param>
        /// <returns></returns>
        public virtual bool Move(int X, int Y, Map Map)
        {
            //no moving out of bounds
            if (!Map.InRange(this.X + X, this.Y + Y))
                return false;
            //phasing actors can go anywhere on the map
            //otherwise check target tile
            if (!CanPhase && !Map.Passable(this.X+X,this.Y+Y))
                return false;
            //check if square already contains something
            MapObject m = Map.ItemAt(this.X + X, this.Y + Y);
            if (m!=this&& !CanPhase&&m!=null)
                return false;
            //phasing or passable tile
            int destX = this.X + X;
            int destY = this.Y + Y;
            this.X += X;
            this.Y += Y;
            return true;
        }

        public void UpdateBars()
        {
            //#TODO: hp/mp regen, status effects, anything that affects these that isn't direct effect
        }

        public override void Tick(float dT)
        {
            if (this.Bars["HP"] <= 0)
                this.Die();
            base.Tick(dT);
        }
        public virtual void Die()
        {
            this.IsDead = true;
        }
    }
}
