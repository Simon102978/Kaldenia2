namespace Server.Items
{
    [Flipable(0x1BD7, 0x1BDA)]
    public class BaseWoodBoard : Item, ICommodity, IResource
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set { m_Resource = value; InvalidateProperties(); }
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Resource >= CraftResource.OakWood && m_Resource <= CraftResource.YewWood)
                    return 1075052 + ((int)m_Resource - (int)CraftResource.OakWood);

                switch (m_Resource)
                {
                    case CraftResource.Bloodwood: return 1075055;
                    case CraftResource.Frostwood: return 1075056;
                    case CraftResource.Heartwood: return 1075062;   //WHY Osi.  Why?
                }

                return 1015101;
            }
        }

        TextDefinition ICommodity.Description => LabelNumber;

        bool ICommodity.IsDeedable => true;

        [Constructable]
        public BaseWoodBoard()
            : this(1)
        {
        }

        [Constructable]
        public BaseWoodBoard(int amount)
            : this(CraftResource.RegularWood, amount)
        {
        }

        public BaseWoodBoard(Serial serial)
            : base(serial)
        {
        }

        [Constructable]
        public BaseWoodBoard(CraftResource resource) : this(resource, 1)
        {
        }

        [Constructable]
        public BaseWoodBoard(CraftResource resource, int amount)
            : base(0x1BD7)
        {
            Stackable = true;
            Amount = amount;

            m_Resource = resource;
            Hue = CraftResources.GetHue(resource);
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

			list.Add(CraftResources.GetName(m_Resource));
		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(4);

            writer.Write((int)m_Resource);
        }

        public static bool UpdatingBaseClass;
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version == 3)
                UpdatingBaseClass = true;
            switch (version)
            {
                case 4:
                case 3:
                case 2:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
            }

            if ((version == 0 && Weight == 0.1) || (version <= 2 && Weight == 2))
                Weight = -1;

            if (version <= 1)
                m_Resource = CraftResource.RegularWood;
        }
    }

	public class RegularBoard : BaseWoodBoard
    {
        [Constructable]
        public RegularBoard()
            : this(1)
        {
        }

        [Constructable]
        public RegularBoard(int amount)
            : base(CraftResource.RegularWood, amount)
        {
        }

        public RegularBoard(Serial serial)
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
            if (UpdatingBaseClass)
                return;
            int version = reader.ReadInt();
        }
    }


	public class PlainoisBoard : BaseWoodBoard
	{
		[Constructable]
		public PlainoisBoard()
			: this(1)
		{
		}

		[Constructable]
		public PlainoisBoard(int amount)
			: base(CraftResource.PlainoisWood, amount)
		{
		}

		public PlainoisBoard(Serial serial)
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
			if (UpdatingBaseClass)
				return;
			int version = reader.ReadInt();
		}
	}

	public class HeartwoodBoard : BaseWoodBoard
    {
        [Constructable]
        public HeartwoodBoard()
            : this(1)
        {
        }

        [Constructable]
        public HeartwoodBoard(int amount)
            : base(CraftResource.Heartwood, amount)
        {
        }

        public HeartwoodBoard(Serial serial)
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

    public class BloodwoodBoard : BaseWoodBoard
    {
        [Constructable]
        public BloodwoodBoard()
            : this(1)
        {
        }

        [Constructable]
        public BloodwoodBoard(int amount)
            : base(CraftResource.Bloodwood, amount)
        {
        }

        public BloodwoodBoard(Serial serial)
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

    public class FrostwoodBoard : BaseWoodBoard
    {
        [Constructable]
        public FrostwoodBoard()
            : this(1)
        {
        }

        [Constructable]
        public FrostwoodBoard(int amount)
            : base(CraftResource.Frostwood, amount)
        {
        }

        public FrostwoodBoard(Serial serial)
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

    public class OakBoard : BaseWoodBoard
    {
        [Constructable]
        public OakBoard()
            : this(1)
        {
        }

        [Constructable]
        public OakBoard(int amount)
            : base(CraftResource.OakWood, amount)
        {
        }

        public OakBoard(Serial serial)
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

    public class AshBoard : BaseWoodBoard
    {
        [Constructable]
        public AshBoard()
            : this(1)
        {
        }

        [Constructable]
        public AshBoard(int amount)
            : base(CraftResource.AshWood, amount)
        {
        }

        public AshBoard(Serial serial)
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

    public class YewBoard : BaseWoodBoard
    {
        [Constructable]
        public YewBoard()
            : this(1)
        {
        }

        [Constructable]
        public YewBoard(int amount)
            : base(CraftResource.YewWood, amount)
        {
        }

        public YewBoard(Serial serial)
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

	public class ForestierBoard : BaseWoodBoard
	{
		[Constructable]
		public ForestierBoard()
			: this(1)
		{
		}

		[Constructable]
		public ForestierBoard(int amount)
			: base(CraftResource.ForestierWood, amount)
		{
		}

		public ForestierBoard(Serial serial)
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

	public class CollinoisBoard : BaseWoodBoard
	{
		[Constructable]
		public CollinoisBoard()
			: this(1)
		{
		}

		[Constructable]
		public CollinoisBoard(int amount)
			: base(CraftResource.CollinoisWood, amount)
		{
		}

		public CollinoisBoard(Serial serial)
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

	public class DesertiqueBoard : BaseWoodBoard
	{
		[Constructable]
		public DesertiqueBoard()
			: this(1)
		{
		}

		[Constructable]
		public DesertiqueBoard(int amount)
			: base(CraftResource.DesertiqueWood, amount)
		{
		}

		public DesertiqueBoard(Serial serial)
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

	public class MontagnardBoard : BaseWoodBoard
	{
		[Constructable]
		public MontagnardBoard()
			: this(1)
		{
		}

		[Constructable]
		public MontagnardBoard(int amount)
			: base(CraftResource.MontagnardWood, amount)
		{
		}

		public MontagnardBoard(Serial serial)
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

	public class SavanoisBoard : BaseWoodBoard
	{
		[Constructable]
		public SavanoisBoard()
			: this(1)
		{
		}

		[Constructable]
		public SavanoisBoard(int amount)
			: base(CraftResource.SavanoisWood, amount)
		{
		}

		public SavanoisBoard(Serial serial)
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

	public class VolcaniqueBoard : BaseWoodBoard
	{
		[Constructable]
		public VolcaniqueBoard()
			: this(1)
		{
		}

		[Constructable]
		public VolcaniqueBoard(int amount)
			: base(CraftResource.VolcaniqueWood, amount)
		{
		}

		public VolcaniqueBoard(Serial serial)
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

	public class ToundroisBoard : BaseWoodBoard
	{
		[Constructable]
		public ToundroisBoard()
			: this(1)
		{
		}

		[Constructable]
		public ToundroisBoard(int amount)
			: base(CraftResource.ToundroisWood, amount)
		{
		}

		public ToundroisBoard(Serial serial)
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

	public class TropicauxBoard : BaseWoodBoard
	{
		[Constructable]
		public TropicauxBoard()
			: this(1)
		{
		}

		[Constructable]
		public TropicauxBoard(int amount)
			: base(CraftResource.TropicauxWood, amount)
		{
		}

		public TropicauxBoard(Serial serial)
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

	public class AncienBoard : BaseWoodBoard
	{
		[Constructable]
		public AncienBoard()
			: this(1)
		{
		}

		[Constructable]
		public AncienBoard(int amount)
			: base(CraftResource.AncienWood, amount)
		{
		}

		public AncienBoard(Serial serial)
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
