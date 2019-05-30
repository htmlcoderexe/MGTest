using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameObjects.MapObjects
{
    public class Monster:Actor, TimeSystem.IActor
    {

        public Monster()
        {
            this.Icon = 2;
            this.Friendliness = FriendlinessValue.Hostile;
            this.Name = "goblin of doom";
        }

        public float Readiness { get; set; }
        public float FillRate { get; set; }

        public void Act()
        {
            if (this.Target != null)
            {
                Point p = GetNextStep(Target.X, Target.Y, ParentMap);

                if (this.X+p.X == Target.X && this.Y+p.Y == Target.Y)
                    Target.TakeDamage(1);

                RequestMove(p.X, p.Y, ParentMap, this.Speed);
            }
        }

    }
}
