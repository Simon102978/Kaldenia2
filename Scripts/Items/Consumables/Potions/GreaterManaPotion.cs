namespace Server.Items
{
    public class GreaterManaPotion : BaseManaPotion
    {
        [Constructable]
        public GreaterManaPotion()
            : base(PotionEffect.ManaGreater)
        {
			Name = "Potion de mana majeure";
			Hue = 2079;

		}

		public GreaterManaPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinManaHeal => 20;
        public override int MaxManaHeal => 30;
        public override double Delay => 16.0;
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
