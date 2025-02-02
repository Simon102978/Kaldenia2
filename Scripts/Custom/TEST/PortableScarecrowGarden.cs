using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
	public class PortableGardenScarecrow : BaseGarden
	{
		private int m_Range;

		[CommandProperty(AccessLevel.GameMaster)]
		public int Range
		{
			get { return m_Range; }
			set
			{
				m_Range = value;
				InvalidateProperties();
				UpdateRegion();
			}
		}

		public override Rectangle2D[] Area
		{
			get { return new Rectangle2D[] { new Rectangle2D(-m_Range, -m_Range, m_Range * 2 + 1, m_Range * 2 + 1) }; }
		}

		[Constructable]
		public PortableGardenScarecrow() : this(null)
		{
		}

		[Constructable]
		public PortableGardenScarecrow(Mobile owner) : base(owner)
		{
			ItemID = 0x1E34;
			Name = "…pouvantail";
			Movable = false;
			Visible = true;
			m_Range = 5; // Default range of 5 tiles (11x11 area)
			Public = true; 
			UpdateRegion();
		}

		public override void OnDoubleClick(Mobile from)
		{
			from.SendGump(new PropertiesGump(from, this));
		}
		public bool IsInRange(Point3D location)
		{
			int dx = Math.Abs(location.X - this.X);
			int dy = Math.Abs(location.Y - this.Y);
			return dx <= m_Range && dy <= m_Range;
		}

		public override void OnLocationChange(Point3D oldLocation)
		{
			base.OnLocationChange(oldLocation);
			UpdateRegion();
		}

		public override void OnMapChange()
		{
			base.OnMapChange();
			UpdateRegion();
		}

		public override BaseGardenDeed Deed()
		{
			return new PortableGardenScarecrowDeed();
		}

		public PortableGardenScarecrow(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_Range);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Range = reader.ReadInt();
		}
	}

	public class PortableGardenScarecrowDeed : BaseGardenDeed
	{
		[Constructable]
		public PortableGardenScarecrowDeed() : base()
		{
			Name = "Portable Garden Scarecrow Deed";
		}

		public override BaseGarden GetGarden(Mobile from)
		{
			return new PortableGardenScarecrow(from);
		}

		public PortableGardenScarecrowDeed(Serial serial) : base(serial) { }

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
