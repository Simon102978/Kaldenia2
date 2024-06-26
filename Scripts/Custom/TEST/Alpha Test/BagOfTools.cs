namespace Server.Items
{
    public class BagofTools : Bag
    {
        [Constructable]
        public BagofTools()
            : this(50)
        {
        }

        [Constructable]
        public BagofTools(int amount)
        {
        
			DropItem(new SewingKit(amount));
      		DropItem(new SmithHammer(amount));
			DropItem(new LeatherSewingKit(amount));
			DropItem(new BoneSewingKit(amount));
			DropItem(new Skillet(amount));
			DropItem(new ScribesPen(amount));
			DropItem(new MortarPestle(amount));
			DropItem(new MortarPestlePoisoning(amount));
			DropItem(new FletcherTools(amount));
			DropItem(new Pinceaux(amount));
			DropItem(new TinkerTools(amount));


		}

		public BagofTools(Serial serial)
            : base(serial)
        {
        }

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