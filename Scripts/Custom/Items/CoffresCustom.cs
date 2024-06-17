using Server.Accounting;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using System.Collections.Generic;
using System.Linq;

namespace Server.Items
{
	[Furniture]
	[Flipable(0xA00E, 0xA00F)]
	public class CoffreMaritime : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x44; } }
		public override int DefaultMaxItems { get { return 250; } }
		[Constructable]
		public CoffreMaritime()
			: base(0xA00E)
		{
			Name = "Coffre Maritime";
			Weight = 10.0;
		}

		public CoffreMaritime(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable(0x9955, 0x9956)]
	public class BarrildeVin : BaseContainer
	{
		public override int DefaultGumpID { get { return 0x3E; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public BarrildeVin()
			: base(0x9955)
		{
			Name = "Petit Barril";
				Weight = 20.0;
		}

		public BarrildeVin(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable(0xA016, 0xA017, 0x9FE7, 0x9FE8)]
	public class CoffreFort : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x4B; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public CoffreFort()
			: base(0xA016)
		{
			Name = "Coffre Fort";
			Weight = 10.0;
		}

		public CoffreFort(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Furniture]
	[Flipable(0xA014, 0xA015)]
	public class CoffreMetalVisqueux : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x4A; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public CoffreMetalVisqueux()
			: base(0xA014)
		{
			Name = "Coffre Visqueux";
			Weight = 10.0;
		}

		public CoffreMetalVisqueux(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Furniture]
	[Flipable(0xA010, 0xA011)]
	public class CoffreMetalRouille : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x4A; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public CoffreMetalRouille()
			: base(0xA010)
		{
			Name = "Coffre Rouillé";
			Weight = 10.0;
		}

		public CoffreMetalRouille(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Furniture]
	[Flipable(0xA012, 0xA013)]
	public class CoffreMetalDore : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x4A; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public CoffreMetalDore()
			: base(0xA012)
		{
			Name = "Coffre Doré";
			Weight = 10.0;
		}

		public CoffreMetalDore(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Furniture]
	[Flipable(0x9972, 0x9973)]
	public class CoffrePirate : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x4A; } }
		public override int DefaultMaxItems { get { return 250; } }

		[Constructable]
		public CoffrePirate()
			: base(0x9972)
		{
			Name = "Coffre";
			Weight = 50.0;
		}
	
		

		public CoffrePirate(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}