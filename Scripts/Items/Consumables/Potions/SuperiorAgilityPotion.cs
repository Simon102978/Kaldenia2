using System;

namespace Server.Items
{
    public class SuperiorAgilityPotion : BaseAgilityPotion
    {
        [Constructable]
        public SuperiorAgilityPotion()
            : base(PotionEffect.AgilitySuperior)
        {
			Name = "Potion de dext�rit� sup�rieure";
		}

		public SuperiorAgilityPotion(Serial serial)
            : base(serial)
        {
        }

        public override int DexOffset => 35;
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