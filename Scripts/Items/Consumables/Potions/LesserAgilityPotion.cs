using System;

namespace Server.Items
{
    public class LesserAgilityPotion : BaseAgilityPotion
    {
        [Constructable]
        public LesserAgilityPotion()
            : base(PotionEffect.AgilityLesser)
        {
			Name = "Potion de dextérité mineure";
		}

		public LesserAgilityPotion(Serial serial)
            : base(serial)
        {
        }

        public override int DexOffset => 5;
        public override TimeSpan Duration => TimeSpan.FromMinutes(5.0);
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