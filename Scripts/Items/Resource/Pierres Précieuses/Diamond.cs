namespace Server.Items
{
    public class Diamant : Item, IGem
    {
        [Constructable]
        public Diamant()
            : this(1)
        {
        }

        [Constructable]
        public Diamant(int amount)
            : base(0xF26)
        {
            Stackable = true;
            Amount = amount;
			Name = "Diamant";
        }

        public Diamant(Serial serial)
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