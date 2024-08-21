using System;

namespace Server.Items
{
    public class LesserIntelligencePotion : BaseIntelligencePotion
    {
        [Constructable]
        public LesserIntelligencePotion()
            : base(PotionEffect.IntelligenceLesser)
        {
			Name = "Potion d'intelligence mineure";
			Hue = 2080;

		}

		public LesserIntelligencePotion(Serial serial)
            : base(serial)
        {
        }

        public override int IntOffset => 10;
		public override TimeSpan MinimumDuration => TimeSpan.FromMinutes(5.0);
		// public override TimeSpan Duration => TimeSpan.FromMinutes(5.0);
		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}