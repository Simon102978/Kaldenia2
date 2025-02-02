namespace Server.Items
{
    public class OfficialSealingWax : Item, IDyable
    {
        public override int LabelNumber => 1072744; // Official Sealing Wax

        [Constructable]
        public OfficialSealingWax()
            : base(0x1426)
        {
            LootType = LootType.Blessed;
            Weight = 1.0;
            Hue = 0x84;
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OfficialSealingWax(Serial serial)
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