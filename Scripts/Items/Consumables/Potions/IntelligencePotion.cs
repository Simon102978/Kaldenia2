using System;

namespace Server.Items
{
    public class IntelligencePotion : BaseIntelligencePotion
    {
        [Constructable]
        public IntelligencePotion()
            : base(PotionEffect.Intelligence)
        {
			Name = "Potion d'intelligence";
			Hue = 2080;

		}

		public IntelligencePotion(Serial serial)
            : base(serial)
        {
        }

        public override int IntOffset => 15;
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