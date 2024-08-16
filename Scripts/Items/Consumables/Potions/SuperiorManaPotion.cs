namespace Server.Items
{
    public class SuperiorManaPotion : BaseManaPotion
    {
        [Constructable]
        public SuperiorManaPotion()
            : base(PotionEffect.ManaSuperior)
        {
			Name = "Potion de mana supérieure";
		}

		public SuperiorManaPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinManaHeal => 30;
        public override int MaxManaHeal => 40;
        public override double Delay => 20.0;
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
