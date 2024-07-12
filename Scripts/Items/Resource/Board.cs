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
            : this(CraftResource.PalmierWood, amount)
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
                m_Resource = CraftResource.PalmierWood;
        }
    }

	public class PalmierBoard : BaseWoodBoard
    {
        [Constructable]
        public PalmierBoard()
            : this(1)
        {
        }

        [Constructable]
        public PalmierBoard(int amount)
            : base(CraftResource.PalmierWood, amount)
        {
        }

        public PalmierBoard(Serial serial)
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


	public class ÉrableBoard : BaseWoodBoard
	{
		[Constructable]
		public ÉrableBoard()
			: this(1)
		{
		}

		[Constructable]
		public ÉrableBoard(int amount)
			: base(CraftResource.ÉrableWood, amount)
		{
		}

		public ÉrableBoard(Serial serial)
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

	public class ChêneBoard : BaseWoodBoard
	{
		[Constructable]
		public ChêneBoard()
			: this(1)
		{
		}

		[Constructable]
		public ChêneBoard(int amount)
			: base(CraftResource.ChêneWood, amount)
		{
		}

		public ChêneBoard(Serial serial)
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

	public class CèdreBoard : BaseWoodBoard
	{
		[Constructable]
		public CèdreBoard()
			: this(1)
		{
		}

		[Constructable]
		public CèdreBoard(int amount)
			: base(CraftResource.CèdreWood, amount)
		{
		}

		public CèdreBoard(Serial serial)
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

	public class CyprèsBoard : BaseWoodBoard
	{
		[Constructable]
		public CyprèsBoard()
			: this(1)
		{
		}

		[Constructable]
		public CyprèsBoard(int amount)
			: base(CraftResource.CyprèsWood, amount)
		{
		}

		public CyprèsBoard(Serial serial)
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

	public class AcajouBoard : BaseWoodBoard
	{
		[Constructable]
		public AcajouBoard()
			: this(1)
		{
		}

		[Constructable]
		public AcajouBoard(int amount)
			: base(CraftResource.AcajouWood, amount)
		{
		}

		public AcajouBoard(Serial serial)
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

	public class SauleBoard : BaseWoodBoard
	{
		[Constructable]
		public SauleBoard()
			: this(1)
		{
		}

		[Constructable]
		public SauleBoard(int amount)
			: base(CraftResource.SauleWood, amount)
		{
		}

		public SauleBoard(Serial serial)
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

	public class ÉbèneBoard : BaseWoodBoard
	{
		[Constructable]
		public ÉbèneBoard()
			: this(1)
		{
		}

		[Constructable]
		public ÉbèneBoard(int amount)
			: base(CraftResource.ÉbèneWood, amount)
		{
		}

		public ÉbèneBoard(Serial serial)
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

	public class PinBoard : BaseWoodBoard
	{
		[Constructable]
		public PinBoard()
			: this(1)
		{
		}

		[Constructable]
		public PinBoard(int amount)
			: base(CraftResource.PinWood, amount)
		{
		}

		public PinBoard(Serial serial)
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

	public class AmaranteBoard : BaseWoodBoard
	{
		[Constructable]
		public AmaranteBoard()
			: this(1)
		{
		}

		[Constructable]
		public AmaranteBoard(int amount)
			: base(CraftResource.AmaranteWood, amount)
		{
		}

		public AmaranteBoard(Serial serial)
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
