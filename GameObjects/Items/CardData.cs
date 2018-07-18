using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Items
{
    public class CardData
    {
        public static Dictionary<int, string> Descriptions = new Dictionary<int, string>
        {
            {0, "It will randomly teleport you elsewhere." },
            {1, "As far as you can tell, it appears to be a completely ordinary playing card." },
            {2, "It will greatly improve your luck." },
            {3, "It will set the room ablaze." },
            {4, "It will restore all your health and grant regeneration." },
            {5, "It will reveal the whole floor." },
            {6, "It will freeze the whole room." },
            {7, "It will have whatever effect you choose." },
            {8, "It will tame the monster you attacked last." },
            {9, "It will make you temporarily invincible." },
            {10, "It will summon a random elemental to fight at your side." },
            {11, "It will improve an item of your choosing." },
            {12, "It will grant you a random amount of gold." },
            {13, "It will summon a few neutral monsters." },
            {14, "It will fully recharge all your wands and magical items." },
            {15, "It can be turned into any card with a magic marker." },
            {16, "It will randomly teleport you elsewhere." },
            {17, "It will randomly teleport you elsewhere." },
            {18, "It will randomly teleport you elsewhere." },
            {19, "It will randomly teleport you elsewhere." },
            {20, "It will randomly teleport you elsewhere." },
            {21, "It will randomly teleport you elsewhere." },
            {22, "It will randomly teleport you elsewhere." },
            {23, "It will have an effect of a randomly chosen card." },
            {24, "It will summon a MagiDeck representative at your location." },
            {25, "It will randomly teleport you elsewhere." },
            {26, "It will deal a lot of damage to everyone in the current room." },
            {27, "It will randomly teleport you elsewhere." },
            {28, "It will randomly teleport you elsewhere." },
            {29, "It will randomly teleport you elsewhere." },
            {30, "It will randomly teleport you elsewhere." },
            {31, "It will grant you discounts in a shop of your choosing." },
        };

        public static bool Apply(MapObjects.Actor Target)
        {

            return true;
        }

        
    }
}
