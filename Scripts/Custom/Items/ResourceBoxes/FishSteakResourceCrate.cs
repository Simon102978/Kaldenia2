using Server;


namespace Server.Items
{

	public class FishSteakResourceCrate : Item //BaseJet
	{
		[Constructable]
		public FishSteakResourceCrate() : base(0x44D0)
		{

			Name = "Caisse de poissons frais";
			Weight = 50.0;
		}

		public FishSteakResourceCrate(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}