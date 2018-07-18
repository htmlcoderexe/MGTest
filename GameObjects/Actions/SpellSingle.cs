using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSystem;

namespace GameObjects.Actions
{
    public class SpellSingle : IEvent
    {
        public IActor Owner { get; set; }

        public int Time { get; set; }

        public MapObjects.Actor Target;

        public Spell Spell;

        public bool Fire()
        {
            return Spell.Cast(Target);
        }
    }
}
