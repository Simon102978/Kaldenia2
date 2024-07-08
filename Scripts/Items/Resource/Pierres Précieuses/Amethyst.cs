namespace Server.Items
{
    public class Amethyste : Item, IGem
    {
        [Constructable]
        public Amethyste()
            : this(1)
        {
        }

        [Constructable]
        public Amethyste(int amount)
            : base(0xF16)
        {
            Stackable = true;
            Amount = amount;
			Name = "Améthyste";
		}

        public Amethyste(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight => 1.0;
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