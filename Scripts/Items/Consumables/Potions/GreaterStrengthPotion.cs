using System;

namespace Server.Items
{
    public class GreaterStrengthPotion : BaseStrengthPotion
    {
        [Constructable]
        public GreaterStrengthPotion()
            : base(PotionEffect.StrengthGreater)
        {
			Name = "Potion de force majeure";
		}

		public GreaterStrengthPotion(Serial serial)
            : base(serial)
        {
        }

        public override int StrOffset => 25;
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