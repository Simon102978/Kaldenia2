
namespace Server.Items
{
	public abstract class BaseGranite : Item, ICommodity
	{
		private CraftResource m_Resource;
		public BaseGranite(CraftResource resource)
			: base(0x1779)
		{
			Hue = CraftResources.GetHue(resource);
			Stackable = true;

			m_Resource = resource;
		}

		public BaseGranite(Serial serial)
			: base(serial)
		{
		}

		TextDefinition ICommodity.Description => LabelNumber;
		bool ICommodity.IsDeedable => true;

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
		public override double DefaultWeight => 1.0;
		public override int LabelNumber => 1044607;// high quality granite
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
				case 1:
				case 0:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (!CraftResources.IsStandard(m_Resource))
			{
				int num = CraftResources.GetLocalizationNumber(m_Resource);

				if (num > 0)
					list.Add(num);
				else
					list.Add(CraftResources.GetName(m_Resource));
			}
		}
	}

	public class Granite : BaseGranite
	{
		[Constructable]
		public Granite()
			: this(1)
		{
		}

		[Constructable]
		public Granite(int amount)
			: base(CraftResource.Iron)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public Granite(Serial serial)
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

	public class DullCopperGranite : BaseGranite
	{
		[Constructable]
		public DullCopperGranite()
			: this(1)
		{
		}

		[Constructable]
		public DullCopperGranite(int amount)
			: base(CraftResource.DullCopper)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public DullCopperGranite(Serial serial)
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

	public class ShadowIronGranite : BaseGranite
	{
		[Constructable]
		public ShadowIronGranite()
			: this(1)
		{
		}

		[Constructable]
		public ShadowIronGranite(int amount)
			: base(CraftResource.ShadowIron)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public ShadowIronGranite(Serial serial)
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



	public class AgapiteGranite : BaseGranite
	{
		[Constructable]
		public AgapiteGranite()
			: this(1)
		{
		}

		[Constructable]
		public AgapiteGranite(int amount)
			: base(CraftResource.Agapite)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public AgapiteGranite(Serial serial)
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

	public class VeriteGranite : BaseGranite
	{
		[Constructable]
		public VeriteGranite()
			: this(1)
		{
		}

		[Constructable]
		public VeriteGranite(int amount)
			: base(CraftResource.Verite)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public VeriteGranite(Serial serial)
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

	public class ValoriteGranite : BaseGranite
	{
		[Constructable]
		public ValoriteGranite()
			: this(1)
		{
		}

		[Constructable]
		public ValoriteGranite(int amount)
			: base(CraftResource.Valorite)
		{
			if (Stackable)
				Amount = amount;
			else
				Amount = 1;
		}

		public ValoriteGranite(Serial serial)
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
		public class BronzeGranite : BaseGranite
		{
			[Constructable]
			public BronzeGranite() : this(1) { }

			[Constructable]
			public BronzeGranite(int amount) : base(CraftResource.Bronze)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public BronzeGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class CopperGranite : BaseGranite
		{
			[Constructable]
			public CopperGranite() : this(1) { }

			[Constructable]
			public CopperGranite(int amount) : base(CraftResource.Copper)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public CopperGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class SonneGranite : BaseGranite
		{
			[Constructable]
			public SonneGranite() : this(1) { }

			[Constructable]
			public SonneGranite(int amount) : base(CraftResource.Sonne)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public SonneGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class ArgentGranite : BaseGranite
		{
			[Constructable]
			public ArgentGranite() : this(1) { }

			[Constructable]
			public ArgentGranite(int amount) : base(CraftResource.Argent)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public ArgentGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class BorealeGranite : BaseGranite
		{
			[Constructable]
			public BorealeGranite() : this(1) { }

			[Constructable]
			public BorealeGranite(int amount) : base(CraftResource.Boreale)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public BorealeGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class ChrysteliarGranite : BaseGranite
		{
			[Constructable]
			public ChrysteliarGranite() : this(1) { }

			[Constructable]
			public ChrysteliarGranite(int amount) : base(CraftResource.Chrysteliar)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public ChrysteliarGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class GlaciasGranite : BaseGranite
		{
			[Constructable]
			public GlaciasGranite() : this(1) { }

			[Constructable]
			public GlaciasGranite(int amount) : base(CraftResource.Glacias)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public GlaciasGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class LithiarGranite : BaseGranite
		{
			[Constructable]
			public LithiarGranite() : this(1) { }

			[Constructable]
			public LithiarGranite(int amount) : base(CraftResource.Lithiar)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public LithiarGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class AcierGranite : BaseGranite
		{
			[Constructable]
			public AcierGranite() : this(1) { }

			[Constructable]
			public AcierGranite(int amount) : base(CraftResource.Acier)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public AcierGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class DurianGranite : BaseGranite
		{
			[Constructable]
			public DurianGranite() : this(1) { }

			[Constructable]
			public DurianGranite(int amount) : base(CraftResource.Durian)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public DurianGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class EquilibrumGranite : BaseGranite
		{
			[Constructable]
			public EquilibrumGranite() : this(1) { }

			[Constructable]
			public EquilibrumGranite(int amount) : base(CraftResource.Equilibrum)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public EquilibrumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class GoldGranite : BaseGranite
		{
			[Constructable]
			public GoldGranite() : this(1) { }

			[Constructable]
			public GoldGranite(int amount) : base(CraftResource.Gold)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public GoldGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class JolinarGranite : BaseGranite
		{
			[Constructable]
			public JolinarGranite() : this(1) { }

			[Constructable]
			public JolinarGranite(int amount) : base(CraftResource.Jolinar)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public JolinarGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class JusticiumGranite : BaseGranite
		{
			[Constructable]
			public JusticiumGranite() : this(1) { }

			[Constructable]
			public JusticiumGranite(int amount) : base(CraftResource.Justicium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public JusticiumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class AbyssiumGranite : BaseGranite
		{
			[Constructable]
			public AbyssiumGranite() : this(1) { }

			[Constructable]
			public AbyssiumGranite(int amount) : base(CraftResource.Abyssium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public AbyssiumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class BloodiriumGranite : BaseGranite
		{
			[Constructable]
			public BloodiriumGranite() : this(1) { }

			[Constructable]
			public BloodiriumGranite(int amount) : base(CraftResource.Bloodirium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public BloodiriumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class HerbrositeGranite : BaseGranite
		{
			[Constructable]
			public HerbrositeGranite() : this(1) { }

			[Constructable]
			public HerbrositeGranite(int amount) : base(CraftResource.Herbrosite)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public HerbrositeGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class KhandariumGranite : BaseGranite
		{
			[Constructable]
			public KhandariumGranite() : this(1) { }

			[Constructable]
			public KhandariumGranite(int amount) : base(CraftResource.Khandarium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public KhandariumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class MytherilGranite : BaseGranite
		{
			[Constructable]
			public MytherilGranite() : this(1) { }

			[Constructable]
			public MytherilGranite(int amount) : base(CraftResource.Mytheril)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public MytherilGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class SombralirGranite : BaseGranite
		{
			[Constructable]
			public SombralirGranite() : this(1) { }

			[Constructable]
			public SombralirGranite(int amount) : base(CraftResource.Sombralir)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public SombralirGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class DraconyrGranite : BaseGranite
		{
			[Constructable]
			public DraconyrGranite() : this(1) { }

			[Constructable]
			public DraconyrGranite(int amount) : base(CraftResource.Draconyr)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public DraconyrGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class HeptazionGranite : BaseGranite
		{
			[Constructable]
			public HeptazionGranite() : this(1) { }

			[Constructable]
			public HeptazionGranite(int amount) : base(CraftResource.Heptazion)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public HeptazionGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class OceanisGranite : BaseGranite
		{
			[Constructable]
			public OceanisGranite() : this(1) { }

			[Constructable]
			public OceanisGranite(int amount) : base(CraftResource.Oceanis)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public OceanisGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class BraziumGranite : BaseGranite
		{
			[Constructable]
			public BraziumGranite() : this(1) { }

			[Constructable]
			public BraziumGranite(int amount) : base(CraftResource.Brazium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public BraziumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class LuneriumGranite : BaseGranite
		{
			[Constructable]
			public LuneriumGranite() : this(1) { }

			[Constructable]
			public LuneriumGranite(int amount) : base(CraftResource.Lunerium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public LuneriumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class MarinarGranite : BaseGranite
		{
			[Constructable]
			public MarinarGranite() : this(1) { }

			[Constructable]
			public MarinarGranite(int amount) : base(CraftResource.Marinar)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public MarinarGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class NostalgiumGranite : BaseGranite
		{
			[Constructable]
			public NostalgiumGranite() : this(1) { }

			[Constructable]
			public NostalgiumGranite(int amount) : base(CraftResource.Nostalgium)
			{
				if (Stackable)
					Amount = amount;
				else
					Amount = 1;
			}

			public NostalgiumGranite(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}
	}




