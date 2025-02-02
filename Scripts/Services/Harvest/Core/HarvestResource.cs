using System;
using Server.Items;

namespace Server.Engines.Harvest
{
    public class HarvestResource
    {
        public HarvestResource(double reqSkill, double minSkill, double maxSkill, object message, params Type[] types)
        {
            ReqSkill = reqSkill;
            MinSkill = minSkill;
            MaxSkill = maxSkill;
            Types = types;
            SuccessMessage = message;
        }

        public Type[] Types { get; set; }
        public double ReqSkill { get; set; }
        public double MinSkill { get; set; }
        public double MaxSkill { get; set; }
        public object SuccessMessage { get; }

		public Bait Bait { get; set; }
		public int MaxAmount { get; set; }

		public Type SpecificFish { get; set; } // Nouvelle propri�t� pour le poisson sp�cifique associ�

		public HarvestResource(Bait bait, int maxAmount)
		{
			Bait = bait;
			MaxAmount = maxAmount;
		}

		public void SendSuccessTo(Mobile m)
        {
            if (SuccessMessage is int)
                m.SendLocalizedMessage((int)SuccessMessage);
            else if (SuccessMessage is string)
                m.SendMessage((string)SuccessMessage);
        }
    }
}
