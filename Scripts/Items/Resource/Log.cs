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
				case CraftResource.�rableWood:	{ item = new �rableBoard();	break; }
				case CraftResource.Ch�neWood:	{ item = new Ch�neBoard();	break; }
				case CraftResource.C�dreWood:	{ item = new C�dreBoard();	break; }
				case CraftResource.Cypr�sWood:	{ item = new Cypr�sBoard(); break; }
				case CraftResource.SauleWood:	{ item = new SauleBoard();	break; }
				case CraftResource.AcajouWood:	{ item = new AcajouBoard(); break; }
				case CraftResource.�b�neWood:	{ item = new �b�neBoard(); break; }
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

	public class �rableLog : BaseLog
	{
		[Constructable]
		public �rableLog()
			: this(1)
		{
		}

		[Constructable]
		public �rableLog(int amount)
			: base(CraftResource.�rableWood, amount)
		{
		}

		public �rableLog(Serial serial)
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
			if (!TryCreateBoards(from, 0, new �rableBoard()))
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

	public class Ch�neLog : BaseLog
	{
		[Constructable]
		public Ch�neLog()
			: this(1)
		{
		}

		[Constructable]
		public Ch�neLog(int amount)
			: base(CraftResource.Ch�neWood, amount)
		{
		}

		public Ch�neLog(Serial serial)
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
			if (!TryCreateBoards(from, 0, new Ch�neBoard()))
				return false;

			return true;
		}
	}
	public class C�dreLog : BaseLog
	{
		[Constructable]
		public C�dreLog()
			: this(1)
		{
		}

		[Constructable]
		public C�dreLog(int amount)
			: base(CraftResource.C�dreWood, amount)
		{
		}

		public C�dreLog(Serial serial)
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
			if (!TryCreateBoards(from, 0, new C�dreBoard()))
				return false;

			return true;
		}
	}
	public class Cypr�sLog : BaseLog
	{
		[Constructable]
		public Cypr�sLog()
			: this(1)
		{
		}

		[Constructable]
		public Cypr�sLog(int amount)
			: base(CraftResource.Cypr�sWood, amount)
		{
		}

		public Cypr�sLog(Serial serial)
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
			if (!TryCreateBoards(from, 0, new Cypr�sBoard()))
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
	public class �b�neLog : BaseLog
	{
		[Constructable]
		public �b�neLog()
			: this(1)
		{
		}

		[Constructable]
		public �b�neLog(int amount)
			: base(CraftResource.�b�neWood, amount)
		{
		}

		public �b�neLog(Serial serial)
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
			if (!TryCreateBoards(from, 0, new �b�neBoard()))
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
