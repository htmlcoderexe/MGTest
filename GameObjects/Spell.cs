using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Spell
    {
        
        public virtual bool Cast(MapObjects.Actor Target)
        {
            return false;
        }
        public virtual bool Cast(float Angle)
        {
            return false;
        }
        public virtual bool Cast(int X, int Y)
        {
            return false;
        }

    }
}
