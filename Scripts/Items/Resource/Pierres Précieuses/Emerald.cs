namespace Server.Items
{
    public class Emeraude : Item, IGem
    {
        [Constructable]
        public Emeraude()
            : this(1)
        {
        }

        [Constructable]
        public Emeraude(int amount)
            : base(0xF10)
        {
            Stackable = true;
            Amount = amount;
			Name = "Émeraude";
        }

        public Emeraude(Serial serial)
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