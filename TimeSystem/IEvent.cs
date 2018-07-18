using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSystem
{
    public interface IEvent
    {
        int Time { get; set; }
        IActor Owner { get; set; }
        bool Fire();
        
    }
}
