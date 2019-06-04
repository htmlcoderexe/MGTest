using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.MapObjects
{
    public class Particle : MapObject
    {
        public float TimeLeft { get; set; }

        public override void Tick(float dT)
        {

            this.TimeLeft -= dT;
            if (this.TimeLeft <= 0)
                this.IsDead = true;
            base.Tick(dT);
        }
    }
}
