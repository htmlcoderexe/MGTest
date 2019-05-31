using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSystem;

namespace GameObjects.Actions
{
    public class UseItem : TimeSystem.IEvent
    {

        public MapObjects.Actor Target;

        public GameObjects.Item Item;

        public IActor Owner { get; set; }

        public int Time { get; set; }

        public bool Fire()
        {
            return false;// Item.Apply(Target);
        }
    }
}
