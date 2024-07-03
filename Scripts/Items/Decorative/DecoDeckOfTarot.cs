namespace Server.Items
{

	[FlipableAttribute(0x12AB, 0x12AC, 0x12A6, 0x12A5, 0x12A7, 0x12A8, 0x12AA)]
	public class DecoDeckOfTarot : Item
    {
        [Constructable]
        public DecoDeckOfTarot()
            : base(0x12AB)
        {
            Movable = true;
            Stackable = false;
			Name = "Jeu de Tarot";
        }
		
        public DecoDeckOfTarot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}