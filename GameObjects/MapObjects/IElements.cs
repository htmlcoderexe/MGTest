using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.MapObjects
{
    interface IElements
    {
        float Fire { get; set; }
        float Lightning { get; set; }
        float Frost { get; set; }
        float Nature { get; set; }
        float Spirit { get; set; }
        float Void { get; set; }
        float Poison { get; set; }
        float Holy { get; set; }
        float Necro { get; set; }
    }
}
