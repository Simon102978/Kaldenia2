namespace Server.Items
{
    public class SaphirEtoile : Item, IGem
    {
        [Constructable]
        public SaphirEtoile()
            : this(1)
        {
        }

        [Constructable]
        public SaphirEtoile(int amount)
            : base(0x0F0F)
        {
            Stackable = true;
            Amount = amount;
			Name = "Sapphire étoilé";
		}

        public SaphirEtoile(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight => 1.0;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
                ItemID = 0x0F0F;
        }
    }
}