using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;

namespace GameObjects.Items
{
    public class ItemCard : Item
    {

        public static int[] SpecialCards = new int[] {1,24,12,26,7,15,23,31 };

        public int Effect;

        public ItemCard(int Effect)
        {
            this.Effect = Effect;
            this.Icon = Map.CardMappings[Effect]+32;
            this.Name = "Effect #" + Effect + ", Icon #" + this.Icon;
            if (SpecialCards.Contains(this.Effect))
                this.Identified = true;
        }

        public override bool Apply(Actor Target)
        {
            Player.Message(this.GetDescription());
            this.Identified = true;
            return true;
        }

        public override string GetDescription()
        {
            if (!Identified)
                return "It is one of the MagiDeck playing cards. Who knows what will happen if you tear it in half?..";
            if (!SpecialCards.Contains(this.Effect))
                return "It is one of the MagiDeck playing cards. "+CardData.Descriptions[this.Effect];
            return CardData.Descriptions[this.Effect];
        }
    }
}
