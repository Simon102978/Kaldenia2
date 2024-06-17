namespace Server.Items
{
    public class GreaterHealPotion : BaseHealPotion
    {
        [Constructable]
        public GreaterHealPotion()
            : base(PotionEffect.HealGreater)
        {
			Name = "Potion de soin majeure";
		}

		public GreaterHealPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinHeal => 40;
        public override int MaxHeal => 40;
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
