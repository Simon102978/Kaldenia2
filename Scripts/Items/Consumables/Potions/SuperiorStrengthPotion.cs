using System;

namespace Server.Items
{
    public class SuperiorStrengthPotion : BaseStrengthPotion
    {
        [Constructable]
        public SuperiorStrengthPotion()
            : base(PotionEffect.StrengthSuperior)
        {
			Name = "Potion de force supérieure";
		}

		public SuperiorStrengthPotion(Serial serial)
            : base(serial)
        {
        }

        public override int StrOffset => 35;
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