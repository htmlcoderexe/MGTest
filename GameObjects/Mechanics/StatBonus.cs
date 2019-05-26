using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Mechanics
{
    public class StatBonus : ICloneable
    {
        public float FlatValue;
        public float Multiplier;
        public string Type;
        public string Description;
        //stat order is in which order the stats get calculated. As each stage involves both multiplication and addition, the order of those stages matters.
        public StatOrder Order;
        /*The order is as follows: 
         * template: stats inherent to specific actor type, such as starting HP
         * character: stats acquired through leveling up or any other permanent increases
         * equip: stats granted by worn equipment (a helmet giving +10 defense)
         * effect: stats altered through status effects, like an armor break reducing defense
         * the last item is there to "automatically" update the number of stages to iterate, it does not actually do anything
         */
        public enum StatOrder
        {
            Template, Character, Equip, Effect, Count
        }
        /// <summary>
        /// Given a list of stat bonuses, calculates a single stage of a single stat (order).
        /// </summary>
        /// <param name="input">Value of stat so far</param>
        /// <param name="Bonuses">List of stat bonuses, pre-filtered to only contain relevant stat</param>
        /// <param name="Stage">Specific stage to calculate</param>
        /// <returns>Calculated value which can be passed on to next stage if applicable</returns>
        static float CalculateStage(float input, List<StatBonus> Bonuses, StatBonus.StatOrder Stage)
        {
            float result = 0;
            //Filter out all bonuses of the specific stage
            List<StatBonus> stage = Bonuses.FindAll(s => s.Order == Stage).ToList();
            //Add up all additive bonuses
            float flats = stage.Sum(s => s.FlatValue);
            //apply all multipliers to each other
            float multis = 1;
            foreach (StatBonus b in stage)
                multis *= (b.Multiplier + 1); //this way -0.50 correctly works out to a 50% multiplier and a 1.00 to a 200% and they cancel each other out.
            //flat increases are affected by the percentages, a hat that gives +10 HP gives +12 if combined with a +20% HP necklace
            result = (input + flats) * multis;
            return result;
        }
        /// <summary>
        /// Calculates a single stat value given a list of stat bonuses (such as one attached to an Actor or an item).
        /// </summary>
        /// <param name="statname">Stat to calculate</param>
        /// <param name="StatBonuses">Stat bonus array</param>
        /// <returns>Final value of the stat</returns>
        public static float CalculateStat(string statname, List<StatBonus> StatBonuses)
        {
            float result = 0;
            //filter all the stat bonuses to the ones with the correct stat
            List<StatBonus> stats = StatBonuses.FindAll(s => s.Type == statname).ToList();
            //default to 0 if none are found, this is rare and should only happen with exotic stats that aren't part of a character template
            if (stats.Count < 1)
                return 0;
            //do stages in order, this is where the "count" is used
            //yes you can iterate an enum like an int
            for (int i = 0; i < (int)StatBonus.StatOrder.Count; i++)
            {
                result = CalculateStage(result, stats, (StatBonus.StatOrder)i);
            }
            return result;
        }
        public object Clone()
        {
            //it has no object references, ez shallow copy is good enough
            return this.MemberwiseClone();
        }
    }
}
