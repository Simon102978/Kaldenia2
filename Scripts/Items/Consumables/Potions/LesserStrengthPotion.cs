using System;

namespace Server.Items
{
    public class LesserStrengthPotion : BaseStrengthPotion
    {
        [Constructable]
        public LesserStrengthPotion()
            : base(PotionEffect.StrengthLesser)
        {
			Name = "Potion de force mineure";
		}

		public LesserStrengthPotion(Serial serial)
            : base(serial)
        {
        }

        public override int StrOffset => 10;
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