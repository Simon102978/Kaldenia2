namespace Server.Items
{
    public class ManaPotion : BaseManaPotion
    {
        [Constructable]
        public ManaPotion() : base(PotionEffect.Mana)
        {
			Name = "Potion de mana";
		}

		public ManaPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinManaHeal => 15;
        public override int MaxManaHeal => 20;
        public override double Delay => 12.0;
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
