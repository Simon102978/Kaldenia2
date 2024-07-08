namespace Server.Items
{
    public class Rubis : Item, IGem
    {
        [Constructable]
        public Rubis()
            : this(1)
        {
        }

        [Constructable]
        public Rubis(int amount)
            : base(0xF13)
        {
            Stackable = true;
            Amount = amount;
			Name = "Rubis";
        }

        public Rubis(Serial serial)
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