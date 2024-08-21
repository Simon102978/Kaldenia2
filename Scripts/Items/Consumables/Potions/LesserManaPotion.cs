namespace Server.Items
{
    public class LesserManaPotion : BaseManaPotion
    {
        [Constructable]
        public LesserManaPotion()
            : base(PotionEffect.ManaLesser)
        {
			Name = "Potion de mana mineure";
			Hue = 2079;

		}

		public LesserManaPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinManaHeal => 5;
        public override int MaxManaHeal => 10;
        public override double Delay => 8.0;
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
