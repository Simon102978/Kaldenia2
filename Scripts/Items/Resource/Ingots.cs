using System;

namespace Server.Items
{
    public abstract class BaseIngot : Item, ICommodity, IResource
    {
        protected virtual CraftResource DefaultResource => CraftResource.Iron;

        private CraftResource m_Resource;
        public BaseIngot(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseIngot(CraftResource resource, int amount)
            : base(0x1BF2)
        {
            Stackable = true;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);
			Name = "Lingot";

            m_Resource = resource;
        }

        public BaseIngot(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                m_Resource = value;
                InvalidateProperties();
            }
        }
        public override double DefaultWeight => 1;
        public override int LabelNumber
        {
            get
            {
       //         if (m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite)
       //             return 1042684 + (m_Resource - CraftResource.DullCopper);

                return 1042692;
            }
        }
        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

		public override void AddNameProperty(ObjectPropertyList list)
		{
			var name = CraftResources.GetName(m_Resource);

			if (Amount > 1)
				list.Add(String.Format("{3} {0}{1}{2}", "Lingots [", name, "]", Amount)); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add(String.Format("{0}{1}{2}", "Lingot [", name, "]")); // ingots
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(CraftResources.GetName(m_Resource));
		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2: // Reset from Resource System
                    m_Resource = DefaultResource;
                    reader.ReadString();
                    break;
                case 1:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        OreInfo info;

                        switch (reader.ReadInt())
                        {
                            case 0:
                                info = OreInfo.Iron;
                                break;
                            case 1:
                                info = OreInfo.DullCopper;
                                break;
                            case 2:
                                info = OreInfo.ShadowIron;
                                break;
                            case 3:
                                info = OreInfo.Copper;
                                break;
                            case 4:
                                info = OreInfo.Bronze;
                                break;
                            case 5:
                                info = OreInfo.Gold;
                                break;
                            case 6:
                                info = OreInfo.Agapite;
                                break;
                            case 7:
                                info = OreInfo.Verite;
                                break;
                            case 8:
                                info = OreInfo.Valorite;
                                break;
							case 9:
								info = OreInfo.Mytheril;
								break;
							default:
                                info = null;
                                break;
                        }

                        m_Resource = CraftResources.GetFromOreInfo(info);
                        break;
                    }
            }
        }
    }

    [Flipable(0x1BF2, 0x1BEF)]
    public class IronIngot : BaseIngot
    {
        [Constructable]
        public IronIngot()
            : this(1)
        {
        }

        [Constructable]
        public IronIngot(int amount)
            : base(CraftResource.Iron, amount)
        {
        }

        public IronIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class DullCopperIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.DullCopper;

        [Constructable]
        public DullCopperIngot()
            : this(1)
        {
        }

        [Constructable]
        public DullCopperIngot(int amount)
            : base(CraftResource.DullCopper, amount)
        {
        }

        public DullCopperIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class ShadowIronIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.ShadowIron;

        [Constructable]
        public ShadowIronIngot()
            : this(1)
        {
        }

        [Constructable]
        public ShadowIronIngot(int amount)
            : base(CraftResource.ShadowIron, amount)
        {
        }

        public ShadowIronIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class CopperIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Copper;

        [Constructable]
        public CopperIngot()
            : this(1)
        {
        }

        [Constructable]
        public CopperIngot(int amount)
            : base(CraftResource.Copper, amount)
        {
        }

        public CopperIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class BronzeIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Bronze;

        [Constructable]
        public BronzeIngot()
            : this(1)
        {
        }

        [Constructable]
        public BronzeIngot(int amount)
            : base(CraftResource.Bronze, amount)
        {
        }

        public BronzeIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class GoldIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Gold;

        [Constructable]
        public GoldIngot()
            : this(1)
        {
        }

        [Constructable]
        public GoldIngot(int amount)
            : base(CraftResource.Gold, amount)
        {
        }

        public GoldIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class AgapiteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Agapite;

        [Constructable]
        public AgapiteIngot()
            : this(1)
        {
        }

        [Constructable]
        public AgapiteIngot(int amount)
            : base(CraftResource.Agapite, amount)
        {
        }

        public AgapiteIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class VeriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Verite;

        [Constructable]
        public VeriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public VeriteIngot(int amount)
            : base(CraftResource.Verite, amount)
        {
        }

        public VeriteIngot(Serial serial)
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

    [Flipable(0x1BF2, 0x1BEF)]
    public class ValoriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource => CraftResource.Valorite;

        [Constructable]
        public ValoriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public ValoriteIngot(int amount)
            : base(CraftResource.Valorite, amount)
        {
        }

        public ValoriteIngot(Serial serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class MytherilIngot : BaseIngot
	{
		[Constructable]
		public MytherilIngot() : this(1)
		{
		}

		[Constructable]
		public MytherilIngot(int amount) : base(CraftResource.Mytheril, amount)
		{
		}

		public MytherilIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class SonneIngot : BaseIngot
	{
		[Constructable]
		public SonneIngot() : this(1)
		{
		}

		[Constructable]
		public SonneIngot(int amount) : base(CraftResource.Sonne, amount)
		{
		}

		public SonneIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class ArgentIngot : BaseIngot
	{
		[Constructable]
		public ArgentIngot() : this(1)
		{
		}

		[Constructable]
		public ArgentIngot(int amount) : base(CraftResource.Argent, amount)
		{
		}

		public ArgentIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class BorealeIngot : BaseIngot
	{
		[Constructable]
		public BorealeIngot() : this(1)
		{
		}

		[Constructable]
		public BorealeIngot(int amount) : base(CraftResource.Boreale, amount)
		{
		}

		public BorealeIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class ChrysteliarIngot : BaseIngot
	{
		[Constructable]
		public ChrysteliarIngot() : this(1)
		{
		}

		[Constructable]
		public ChrysteliarIngot(int amount) : base(CraftResource.Chrysteliar, amount)
		{
		}

		public ChrysteliarIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class GlaciasIngot : BaseIngot
	{
		[Constructable]
		public GlaciasIngot() : this(1)
		{
		}

		[Constructable]
		public GlaciasIngot(int amount) : base(CraftResource.Glacias, amount)
		{
		}

		public GlaciasIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class LithiarIngot : BaseIngot
	{
		[Constructable]
		public LithiarIngot() : this(1)
		{
		}

		[Constructable]
		public LithiarIngot(int amount) : base(CraftResource.Lithiar, amount)
		{
		}

		public LithiarIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class AcierIngot : BaseIngot
	{
		[Constructable]
		public AcierIngot() : this(1)
		{
		}

		[Constructable]
		public AcierIngot(int amount) : base(CraftResource.Acier, amount)
		{
		}

		public AcierIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class DurianIngot : BaseIngot
	{
		[Constructable]
		public DurianIngot() : this(1)
		{
		}

		[Constructable]
		public DurianIngot(int amount) : base(CraftResource.Durian, amount)
		{
		}

		public DurianIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class EquilibrumIngot : BaseIngot
	{
		[Constructable]
		public EquilibrumIngot() : this(1)
		{
		}

		[Constructable]
		public EquilibrumIngot(int amount) : base(CraftResource.Equilibrum, amount)
		{
		}

		public EquilibrumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class JolinarIngot : BaseIngot
	{
		[Constructable]
		public JolinarIngot() : this(1)
		{
		}

		[Constructable]
		public JolinarIngot(int amount) : base(CraftResource.Jolinar, amount)
		{
		}

		public JolinarIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class JusticiumIngot : BaseIngot
	{
		[Constructable]
		public JusticiumIngot() : this(1)
		{
		}

		[Constructable]
		public JusticiumIngot(int amount) : base(CraftResource.Justicium, amount)
		{
		}

		public JusticiumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class AbyssiumIngot : BaseIngot
	{
		[Constructable]
		public AbyssiumIngot() : this(1)
		{
		}

		[Constructable]
		public AbyssiumIngot(int amount) : base(CraftResource.Abyssium, amount)
		{
		}

		public AbyssiumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class BloodiriumIngot : BaseIngot
	{
		[Constructable]
		public BloodiriumIngot() : this(1)
		{
		}

		[Constructable]
		public BloodiriumIngot(int amount) : base(CraftResource.Bloodirium, amount)
		{
		}

		public BloodiriumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class HerbrositeIngot : BaseIngot
	{
		[Constructable]
		public HerbrositeIngot() : this(1)
		{
		}

		[Constructable]
		public HerbrositeIngot(int amount) : base(CraftResource.Herbrosite, amount)
		{
		}

		public HerbrositeIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class KhandariumIngot : BaseIngot
	{
		[Constructable]
		public KhandariumIngot() : this(1)
		{
		}

		[Constructable]
		public KhandariumIngot(int amount) : base(CraftResource.Khandarium, amount)
		{
		}

		public KhandariumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class SombralirIngot : BaseIngot
	{
		[Constructable]
		public SombralirIngot() : this(1)
		{
		}

		[Constructable]
		public SombralirIngot(int amount) : base(CraftResource.Sombralir, amount)
		{
		}

		public SombralirIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class DraconyrIngot : BaseIngot
	{
		[Constructable]
		public DraconyrIngot() : this(1)
		{
		}

		[Constructable]
		public DraconyrIngot(int amount) : base(CraftResource.Draconyr, amount)
		{
		}

		public DraconyrIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class HeptazionIngot : BaseIngot
	{
		[Constructable]
		public HeptazionIngot() : this(1)
		{
		}

		[Constructable]
		public HeptazionIngot(int amount) : base(CraftResource.Heptazion, amount)
		{
		}

		public HeptazionIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class OceanisIngot : BaseIngot
	{
		[Constructable]
		public OceanisIngot() : this(1)
		{
		}

		[Constructable]
		public OceanisIngot(int amount) : base(CraftResource.Oceanis, amount)
		{
		}

		public OceanisIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class BraziumIngot : BaseIngot
	{
		[Constructable]
		public BraziumIngot() : this(1)
		{
		}

		[Constructable]
		public BraziumIngot(int amount) : base(CraftResource.Brazium, amount)
		{
		}

		public BraziumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class LuneriumIngot : BaseIngot
	{
		[Constructable]
		public LuneriumIngot() : this(1)
		{
		}

		[Constructable]
		public LuneriumIngot(int amount) : base(CraftResource.Lunerium, amount)
		{
		}

		public LuneriumIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class MarinarIngot : BaseIngot
	{
		[Constructable]
		public MarinarIngot() : this(1)
		{
		}

		[Constructable]
		public MarinarIngot(int amount) : base(CraftResource.Marinar, amount)
		{
		}

		public MarinarIngot(Serial serial) : base(serial)
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
	[FlipableAttribute(0x1BF2, 0x1BEF)]
	public class NostalgiumIngot : BaseIngot
	{
		[Constructable]
		public NostalgiumIngot() : this(1)
		{
		}

		[Constructable]
		public NostalgiumIngot(int amount) : base(CraftResource.Nostalgium, amount)
		{
		}

		public NostalgiumIngot(Serial serial) : base(serial)
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
