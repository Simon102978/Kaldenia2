using System;

namespace Server.Items
{
    public class StrengthPotion : BaseStrengthPotion
    {
        [Constructable]
        public StrengthPotion()
            : base(PotionEffect.Strength)
        {
			Name = "Potion de force";
		}

		public StrengthPotion(Serial serial)
            : base(serial)
        {
        }

        public override int StrOffset => 15;
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