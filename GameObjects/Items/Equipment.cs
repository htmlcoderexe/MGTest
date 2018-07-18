using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Items
{
    public class Equipment : Item
    {
        public enum Slot
        {
            Head,
            Torso1,
            Torso2,
            Legs,
            Arms,
            Feet,
            Hands,
            RingL,
            RingR,
            Neck,
            Cloak
        }
    }
}
