using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.MapObjects
{
    interface IAttributes
    {
        int HP { get; set; }
        int Strength { get; set; }
        int Dexterity { get; set; }
        int Vitality { get; set; }
        int Magic { get; set; }
        int Intelligence { get; set; }
        int Speed { get; set; }
        int PhysicalDefence { get; set; }
        int MagicalDefence { get; set; }
    }
}
