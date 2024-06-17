using System;

namespace Server.Items
{
    public class AgilityPotion : BaseAgilityPotion
    {
        [Constructable]
        public AgilityPotion()
            : base(PotionEffect.Agility)
        {
			Name = "Potion de dextérité";
		}

		public AgilityPotion(Serial serial)
            : base(serial)
        {
        }

        public override int DexOffset => 15;
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