namespace Server.Items
{
    [Flipable(0x1bdd, 0x1be0)]
    public class BaseLog : Item, ICommodity, IAxe, IResource
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set { m_Resource = value; InvalidateProperties(); }
        }

        TextDefinition ICommodity.Description => CraftResources.IsStandard(m_Resource) ? LabelNumber : 1075062 + ((int)m_Resource - (int)CraftResource.PalmierWood);
        bool ICommodity.IsDeedable => true;
        [Constructable]
        public BaseLog() : this(1)
        {
        }

        [Constructable]
        public BaseLog(int amount) : this(CraftResource.PalmierWood, amount)
        {
        }

        [Constructable]
        public BaseLog(CraftResource resource)
            : this(resource, 1)
        {
        }
        [Constructable]
        public BaseLog(CraftResource resource, int amount)
            : base(0x1BDD)
        {
            Stackable = true;
            Weight = 2.0;
            Amount = amount;

            m_Resource = resource;
            Hue = CraftResources.GetHue(resource);
        }

		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

			list.Add(CraftResources.GetName(m_Resource));
		}

        public BaseLog(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(2); // version

            writer.Write((int)m_Resource);
        }

        public static bool UpdatingBaseLogClass;
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 1)
                UpdatingBaseLogClass = true;
            m_Resource = (CraftResource)reader.ReadInt();

            if (version == 0)
                m_Resource = CraftResource.PalmierWood;
        }

        public virtual bool TryCreateBoards(Mobile from, double skill, Item item)
        {
            if (Deleted || !from.CanSee(this))
            {
                item.Delete();
                return false;
            }
            if (from.Skills.Carpentry.Value < skill && from.Skills.Lumberjacking.Value < skill)
            {
                item.Delete();
                from.SendLocalizedMessage(1072652); // You cannot work this strange and unusual wood.
                return false;
            }

            if (HasSocket<Caddellite>())
            {
                item.AttachSocket(new Caddellite());
            }

            base.ScissorHelper(from, item, 2, false);
            return true;
        }

        public virtual bool Axe(Mobile from, BaseAxe axe)
        {
			Item item;

			switch(Resource)
			{
				default:
				case CraftResource.PalmierWood:		{ item = new PalmierBoard();	break; }
				case CraftResource.ÉrableWood:	{ item = new ÉrableBoard();	break; }
				case CraftResource.ChêneWood:	{ item = new ChêneBoard();	break; }
				case CraftResource.CèdreWood:	{ item = new CèdreBoard();	break; }
				case CraftResource.CyprèsWood:	{ item = new CyprèsBoard(); break; }
				case CraftResource.SauleWood:	{ item = new SauleBoard();	break; }
				case CraftResource.AcajouWood:	{ item = new AcajouBoard(); break; }
				case CraftResource.ÉbèneWood:	{ item = new ÉbèneBoard(); break; }
				case CraftResource.AmaranteWood:	{ item = new AmaranteBoard();	break; }
				case CraftResource.PinWood:	{ item = new PinBoard();	break; }
				case CraftResource.AncienWood:		{ item = new AncienBoard();		break; }
			}

			if (item != null && !TryCreateBoards(from, 0, item))
                return false;

            return true;
        }
    }

	public class PalmierLog : BaseLog
    {
        [Constructable]
        public PalmierLog()
            : this(1)
        {
        }

        [Constructable]
        public PalmierLog(int amount)
            : base(CraftResource.PalmierWood, amount)
        {
        }

        public PalmierLog(Serial serial)
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
            //don't deserialize anything on update
            if (UpdatingBaseLogClass)
                return;

            int version = reader.ReadInt();
        }

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 0, new PalmierBoard()))
                return false;

            return true;
        }
    }

	public class ÉrableLog : BaseLog
	{
		[Constructable]
		public ÉrableLog()
			: this(1)
		{
		}

		[Constructable]
		public ÉrableLog(int amount)
			: base(CraftResource.ÉrableWood, amount)
		{
		}

		public ÉrableLog(Serial serial)
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
			//don't deserialize anything on update
			if (UpdatingBaseLogClass)
				return;

			int version = reader.ReadInt();
		}

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new ÉrableBoard()))
				return false;

			return true;
		}
	}

	public class HeartwoodLog : BaseLog
    {
        [Constructable]
        public HeartwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public HeartwoodLog(int amount)
            : base(CraftResource.Heartwood, amount)
        {
        }

        public HeartwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 100, new HeartwoodBoard()))
                return false;

            return true;
        }
    }

    public class BloodwoodLog : BaseLog
    {
        [Constructable]
        public BloodwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public BloodwoodLog(int amount)
            : base(CraftResource.Bloodwood, amount)
        {
        }

        public BloodwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 100, new BloodwoodBoard()))
                return false;

            return true;
        }
    }

    public class FrostwoodLog : BaseLog
    {
        [Constructable]
        public FrostwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public FrostwoodLog(int amount)
            : base(CraftResource.Frostwood, amount)
        {
        }

        public FrostwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 100, new FrostwoodBoard()))
                return false;

            return true;
        }
    }

    public class OakLog : BaseLog
    {
        [Constructable]
        public OakLog()
            : this(1)
        {
        }

        [Constructable]
        public OakLog(int amount)
            : base(CraftResource.OakWood, amount)
        {
        }

        public OakLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 65, new OakBoard()))
                return false;

            return true;
        }
    }

    public class AshLog : BaseLog
    {
        [Constructable]
        public AshLog()
            : this(1)
        {
        }

        [Constructable]
        public AshLog(int amount)
            : base(CraftResource.AshWood, amount)
        {
        }

        public AshLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 80, new AshBoard()))
                return false;

            return true;
        }
    }

    public class YewLog : BaseLog
    {
        [Constructable]
        public YewLog()
            : this(1)
        {
        }

        [Constructable]
        public YewLog(int amount)
            : base(CraftResource.YewWood, amount)
        {
        }

        public YewLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 95, new YewBoard()))
                return false;

            return true;
        }
    }

	public class ChêneLog : BaseLog
	{
		[Constructable]
		public ChêneLog()
			: this(1)
		{
		}

		[Constructable]
		public ChêneLog(int amount)
			: base(CraftResource.ChêneWood, amount)
		{
		}

		public ChêneLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new ChêneBoard()))
				return false;

			return true;
		}
	}
	public class CèdreLog : BaseLog
	{
		[Constructable]
		public CèdreLog()
			: this(1)
		{
		}

		[Constructable]
		public CèdreLog(int amount)
			: base(CraftResource.CèdreWood, amount)
		{
		}

		public CèdreLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new CèdreBoard()))
				return false;

			return true;
		}
	}
	public class CyprèsLog : BaseLog
	{
		[Constructable]
		public CyprèsLog()
			: this(1)
		{
		}

		[Constructable]
		public CyprèsLog(int amount)
			: base(CraftResource.CyprèsWood, amount)
		{
		}

		public CyprèsLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new CyprèsBoard()))
				return false;

			return true;
		}
	}
	public class AcajouLog : BaseLog
	{
		[Constructable]
		public AcajouLog()
			: this(1)
		{
		}

		[Constructable]
		public AcajouLog(int amount)
			: base(CraftResource.AcajouWood, amount)
		{
		}

		public AcajouLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new AcajouBoard()))
				return false;

			return true;
		}
	}
	public class SauleLog : BaseLog
	{
		[Constructable]
		public SauleLog()
			: this(1)
		{
		}

		[Constructable]
		public SauleLog(int amount)
			: base(CraftResource.SauleWood, amount)
		{
		}

		public SauleLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new SauleBoard()))
				return false;

			return true;
		}
	}
	public class ÉbèneLog : BaseLog
	{
		[Constructable]
		public ÉbèneLog()
			: this(1)
		{
		}

		[Constructable]
		public ÉbèneLog(int amount)
			: base(CraftResource.ÉbèneWood, amount)
		{
		}

		public ÉbèneLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new ÉbèneBoard()))
				return false;

			return true;
		}
	}
	public class PinLog : BaseLog
	{
		[Constructable]
		public PinLog()
			: this(1)
		{
		}

		[Constructable]
		public PinLog(int amount)
			: base(CraftResource.PinWood, amount)
		{
		}

		public PinLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new PinBoard()))
				return false;

			return true;
		}
	}
	public class AmaranteLog : BaseLog
	{
		[Constructable]
		public AmaranteLog()
			: this(1)
		{
		}

		[Constructable]
		public AmaranteLog(int amount)
			: base(CraftResource.AmaranteWood, amount)
		{
		}

		public AmaranteLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new AmaranteBoard()))
				return false;

			return true;
		}
	}
	public class AncienLog : BaseLog
	{
		[Constructable]
		public AncienLog()
			: this(1)
		{
		}

		[Constructable]
		public AncienLog(int amount)
			: base(CraftResource.AncienWood, amount)
		{
		}

		public AncienLog(Serial serial)
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

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 0, new AncienBoard()))
				return false;

			return true;
		}
	}
}
