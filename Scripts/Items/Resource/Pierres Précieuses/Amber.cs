namespace Server.Items
{
    public class Ambre : Item, ICommodity
    {
        [Constructable]
        public Ambre()
            : this(1)
        {
        }

        [Constructable]
        public Ambre(int amount)
            : base(0xF25)
        {
            Stackable = true;
            Amount = amount;
			Name = "Ambre";
		}

        public Ambre(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

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
