using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSystem
{
    public class SchedulerATB
    {
         List<IActor> actors;
        public Queue<IActor> Sequence;
        public SchedulerATB()
        {
            actors = new List<IActor>();
            Sequence = new Queue<IActor>();
        }
        public void Add(IActor Actor)
        {
            actors.Add(Actor);
        }
        public void Remove(IActor Actor)
        {
            actors.Remove(Actor);
        }
        /// <summary>
        /// Cranks the turns. 
        /// </summary>
        /// <returns>Returns a boolean whether an action has occurred or not which allows it to crank in a loop until something happens.</returns>
        public bool Crank()
        {
            //prevent infinite loops if no actors are present
            if (actors.Count <= 0)
                return true;
            //Start by filling each actor's gauge by their speed value. 
            foreach (IActor actor in actors)
                actor.Readiness += actor.FillRate;
            //Actor with highest value above 1.0 gets selected to act and loses a full action worth of gauge. Specific actions defined by actors can create longer or shorter actions, allowing the actor to act faster after a quick action or later after something slow like casting the kill everyone forever spell.
            actors = actors.OrderByDescending(a => a.Readiness).ToList();
            int i = 0;
            bool nonempty = false;
            while(i<actors.Count)
            {

                IActor aNext = actors[i];
                if (aNext.Readiness < 1.0f)
                    return nonempty;
                aNext.Readiness--;
                aNext.Act();
                nonempty = true;
                i++;
            }
            return true;
        }

        
    }
}
