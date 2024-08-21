using System;

namespace Server.Items
{
    public class GreaterIntelligencePotion : BaseIntelligencePotion
    {
        [Constructable]
        public GreaterIntelligencePotion()
            : base(PotionEffect.IntelligenceGreater)
        {
			Name = "Potion d'intelligence majeure";
			Hue = 2080;

		}

		public GreaterIntelligencePotion(Serial serial)
            : base(serial)
        {
        }

        public override int IntOffset => 25;
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