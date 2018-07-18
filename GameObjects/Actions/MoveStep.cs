using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;

namespace GameObjects.Actions
{
    class MoveStep : TimeSystem.IEvent
    {
        public TimeSystem.IActor Owner { get; set; }

        public int Time { get; set; }

        public int X;
        public int Y;
        public Map Map;

        public bool Fire()
        {
            Actor a = Owner as Actor;
            a.Move(X, Y, Map);
            return true;
        }
    }
}
