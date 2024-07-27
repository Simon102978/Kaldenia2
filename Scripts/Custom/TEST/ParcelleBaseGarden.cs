using Server.Items;
using Server;

public class ParcelleJardin : BaseGarden
{
	public override Rectangle2D[] Area => new Rectangle2D[] { new Rectangle2D(X, Y, 1, 1) };

	public override BaseGardenDeed Deed() => null; 

	[Constructable]
	public ParcelleJardin() : base(null) 
	{
		ItemID = 0x31F5;
		Name = "Terre";
		Movable = false;
	}

	public ParcelleJardin(Serial serial) : base(serial)
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
