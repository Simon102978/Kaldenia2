namespace Server.Items
{
    public class GreaterRefreshPotion : BaseRefreshPotion
    {
        [Constructable]
        public GreaterRefreshPotion() : base(PotionEffect.RefreshGreater)
        {
			Name = "Potion de rafraichissement majeure";
		}

		public GreaterRefreshPotion(Serial serial) : base(serial)
        {
        }

        public override double Refresh => 0.75;
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