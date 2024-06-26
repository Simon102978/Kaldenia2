namespace Server.Items
{
    public class BagOfRessources : Bag
    {
        [Constructable]
        public BagOfRessources()
            : this(50)
        {
        }

        [Constructable]
        public BagOfRessources(int amount)
        {
        
			DropItem(new Bottle(amount));
      		DropItem(new IronIngot(amount));
			DropItem(new RegularBoard(amount));
			DropItem(new Leather(amount));
			DropItem(new BlankScroll(amount));
			DropItem(new Cloth(amount));
			DropItem(new Bone(amount));








		}

		public BagOfRessources(Serial serial)
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