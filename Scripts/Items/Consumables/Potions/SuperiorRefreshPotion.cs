namespace Server.Items
{
    public class SuperiorRefreshPotion : BaseRefreshPotion
    {
        [Constructable]
        public SuperiorRefreshPotion() : base(PotionEffect.RefreshSuperior)
        {
			Name = "Potion de rafraichissement supérieure";
		}

		public SuperiorRefreshPotion(Serial serial) : base(serial)
        {
        }

        public override double Refresh => 1.0;
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