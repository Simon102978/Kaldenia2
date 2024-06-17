namespace Server.Items
{
    public class RefreshPotion : BaseRefreshPotion
    {
        [Constructable]
        public RefreshPotion() : base(PotionEffect.Refresh)
        {
			Name = "Potion de rafraichissement";
		}

		public RefreshPotion(Serial serial)
            : base(serial)
        {
        }

        public override double Refresh => 0.5;
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