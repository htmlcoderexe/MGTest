using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSystem
{
    public class Scheduler
    {
        SortedDictionary<int, List<IEvent>> Queue = new SortedDictionary<int, List<IEvent>>();
        int Time { get; set; }
        public void Add(IEvent Event)
        {
            if(!Queue.ContainsKey(Time+Event.Time))
            {
                Queue.Add(Time + Event.Time,new List<IEvent>());
            }
            Queue[Time + Event.Time].Add(Event);
        }
        public void Remove(IEvent Event)
        {
            //fishing in nested IEnumerables is challenging and dangerous.
            //first, we need the hook, it will hold a reference to our fish.
            KeyValuePair<int, List<IEvent>> hook = new KeyValuePair<int, List<IEvent>>(-1, null);
            //then the drag the hook through the queue in the hopes of brushing against the target.
            foreach(var kvp in Queue)
            {
                if(kvp.Value.Contains(Event))
                {
                    //this contains a reference to the list we can modify, and the index where it is stored.
                    hook = kvp;
                    //after achieving our first goal we deserve a
                    break; 
                }
            }
            //if there's nothing on the hook, there's no point in doing anything
            if(hook.Value!=null)
            {
                //remove the event first
                hook.Value.Remove(Event);
                //if there are no more events on there, nuke the whole thing
                if(hook.Value.Count<=0)
                {
                    Queue.Remove(hook.Key);
                }
            }
        }

        public int Run(KeyValuePair<int,List<IEvent>> Timeslot)
        {
            if (Timeslot.Value.Count <= 1)
                this.Queue.Remove(Timeslot.Key);
            var action =Timeslot.Value.First();
            //remove from scheduler
            Timeslot.Value.Remove(action);
            //jump time to when action is due, nothing happens in between anyway
            int dT = Time - Timeslot.Key;
            Time = Timeslot.Key;
            //fire the action
            action.Fire();
            //notify owner 
            this.Queue.ToList();
            if(action.Owner!=null)
            action.Owner.Act();
            return dT;
        }

        public int GetTime()
        {
            return Time;
        }

        public int Next()
        {
            //bail early if nothing scheduled
            if (Queue.Count <= 0)
                return Time;
            //get the first (always lowest value in SortedDictionary)
            var first = Queue.First();
            //get the first action there
            return Run(first);
        }
        public void Crank(int Times,bool KeepGoing=false)
        { 
            //bail early if nothing scheduled
            if (Queue.Count <= 0)
                return;
            //Get the earliest item for comparing
            var slot = Queue.First();
            //Step through time
            for(int i =0;i<Times;i++)
            {
                //advance time by 1 tick
                Time++;
                //if the earliest item is hit, execute it
                if(Time==slot.Key)
                {
                    Run(slot);
                    //stop here unless asked not to
                    if(!KeepGoing)
                    {
                        break;
                    }
                }
            }
        }

        public void Clear()
        {
            Time = 0;
            Queue.Clear();
        }
    }
}
