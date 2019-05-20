using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSystem
{
    public interface IActor
    {
        void Act();
        //can act when full (>=1.0)
        float Readiness { get; set; }
        //the bigger, the more often it can act
        float FillRate { get; set; }
    }
}
