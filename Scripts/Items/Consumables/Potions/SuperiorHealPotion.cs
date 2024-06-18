namespace Server.Items
{
    public class SuperiorHealPotion : BaseHealPotion
    {
        [Constructable]
        public SuperiorHealPotion()
            : base(PotionEffect.HealSuperior)
        {
			Name = "Potion de soin supérieure";
		}

		public SuperiorHealPotion(Serial serial)
            : base(serial)
        {
        }

        public override int MinHeal => 60;
        public override int MaxHeal => 60;
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
