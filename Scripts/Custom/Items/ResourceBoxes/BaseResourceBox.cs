namespace Server.Items
{
	public abstract class BaseResourceCrate : Item
	{
		protected virtual CraftResource DefaultResource => CraftResource.RegularLeather;

		private CraftResource m_Resource;
		public BaseResourceCrate(CraftResource resource, int itemId) : this(resource, itemId, 1)
		{
		}

		public BaseResourceCrate(CraftResource resource, int itemId, int amount) : base(itemId)
		{
			Name = "Boite de ressources";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Hue = CraftResources.GetHue(resource);

			m_Resource = resource;
		}

		public BaseResourceCrate(Serial serial) : base(serial)
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

		public override void AddNameProperty(ObjectPropertyList list)
		{
			list.Add($"Ressource: {CraftResources.GetName(m_Resource)}");
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version

			writer.Write((int)m_Resource);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}
		}
	}

	

	[Flipable(0xA004, 0xA004)]
	public class PalmierWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PalmierWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PalmierWoodResourceCrate(int amount)
			: base(CraftResource.ErableWood, 0xA004, amount)
		{
		}

		public PalmierWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class ErableWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ErableWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ErableWoodResourceCrate(int amount)
			: base(CraftResource.ErableWood, 0xA004, amount)
		{
		}

		public ErableWoodResourceCrate(Serial serial)
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



	[Flipable(0xA004, 0xA004)]
	public class CheneWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CheneWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CheneWoodResourceCrate(int amount)
			: base(CraftResource.CheneWood, 0xA004, amount)
		{
		}

		public CheneWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class CedreWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CedreWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CedreWoodResourceCrate(int amount)
			: base(CraftResource.CedreWood, 0xA004, amount)
		{
		}

		public CedreWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class CypresWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CypresWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CypresWoodResourceCrate(int amount)
			: base(CraftResource.CypresWood, 0xA004, amount)
		{
		}

		public CypresWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class SauleWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SauleWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SauleWoodResourceCrate(int amount)
			: base(CraftResource.SauleWood, 0xA004, amount)
		{
		}

		public SauleWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AcajouWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AcajouWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AcajouWoodResourceCrate(int amount)
			: base(CraftResource.AcajouWood, 0xA004, amount)
		{
		}

		public AcajouWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class EbeneWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public EbeneWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public EbeneWoodResourceCrate(int amount)
			: base(CraftResource.EbeneWood, 0xA004, amount)
		{
		}

		public EbeneWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AmaranteWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AmaranteWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AmaranteWoodResourceCrate(int amount)
			: base(CraftResource.AmaranteWood, 0xA004, amount)
		{
		}

		public AmaranteWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class PinWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PinWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PinWoodResourceCrate(int amount)
			: base(CraftResource.PinWood, 0xA004, amount)
		{
		}

		public PinWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AncienWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AncienWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AncienWoodResourceCrate(int amount)
			: base(CraftResource.AncienWood, 0xA004, amount)
		{
		}

		public AncienWoodResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class RegularLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public RegularLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public RegularLeatherResourceCrate(int amount)
			: base(CraftResource.RegularLeather, 0xA009, amount)
		{
		}

		public RegularLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class ChêneLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ChêneLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ChêneLeatherResourceCrate(int amount)
			: base(CraftResource.LupusLeather, 0xA009, amount)
		{
		}

		public ChêneLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class CèdreLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CèdreLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CèdreLeatherResourceCrate(int amount)
			: base(CraftResource.LupusLeather, 0xA009, amount)
		{
		}

		public CèdreLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class CyprèsLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CyprèsLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CyprèsLeatherResourceCrate(int amount)
			: base(CraftResource.ReptilienLeather, 0xA009, amount)
		{
		}

		public CyprèsLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class SauleLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SauleLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SauleLeatherResourceCrate(int amount)
			: base(CraftResource.GeantLeather, 0xA009, amount)
		{
		}

		public SauleLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AcajouLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AcajouLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AcajouLeatherResourceCrate(int amount)
			: base(CraftResource.OphidienLeather, 0xA009, amount)
		{
		}

		public AcajouLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class ÉbèneLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ÉbèneLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ÉbèneLeatherResourceCrate(int amount)
			: base(CraftResource.ArachnideLeather, 0xA009, amount)
		{
		}

		public ÉbèneLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AmaranteLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AmaranteLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AmaranteLeatherResourceCrate(int amount)
			: base(CraftResource.DragoniqueLeather, 0xA009, amount)
		{
		}

		public AmaranteLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class PinLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PinLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PinLeatherResourceCrate(int amount)
			: base(CraftResource.DemoniaqueLeather, 0xA009, amount)
		{
		}

		public PinLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA004, 0xA004)]
	public class AncienLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AncienLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AncienLeatherResourceCrate(int amount)
			: base(CraftResource.AncienLeather, 0xA009, amount)
		{
		}

		public AncienLeatherResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class ÉrableBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ÉrableBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ÉrableBoneResourceCrate(int amount)
			: base(CraftResource.RegularBone, 0xA006, amount)
		{
		}

		public ÉrableBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class ChêneBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ChêneBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ChêneBoneResourceCrate(int amount)
			: base(CraftResource.LupusBone, 0xA006, amount)
		{
		}

		public ChêneBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class CèdreBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CèdreBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CèdreBoneResourceCrate(int amount)
			: base(CraftResource.ReptilienBone, 0xA006, amount)
		{
		}

		public CèdreBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class CyprèsBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CyprèsBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CyprèsBoneResourceCrate(int amount)
			: base(CraftResource.GeantBone, 0xA006, amount)
		{
		}

		public CyprèsBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class SauleBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SauleBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SauleBoneResourceCrate(int amount)
			: base(CraftResource.OphidienBone, 0xA006, amount)
		{
		}

		public SauleBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class AcajouBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AcajouBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AcajouBoneResourceCrate(int amount)
			: base(CraftResource.ArachnideBone, 0xA006, amount)
		{
		}

		public AcajouBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class ÉbèneBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ÉbèneBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ÉbèneBoneResourceCrate(int amount)
			: base(CraftResource.DragoniqueBone, 0xA006, amount)
		{
		}

		public ÉbèneBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class AmaranteBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AmaranteBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AmaranteBoneResourceCrate(int amount)
			: base(CraftResource.DemoniaqueBone, 0xA006, amount)
		{
		}

		public AmaranteBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class PinBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PinBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PinBoneResourceCrate(int amount)
			: base(CraftResource.AncienBone, 0xA006, amount)
		{
		}

		public PinBoneResourceCrate(Serial serial)
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

	[Flipable(0xA006, 0xA006)]
	public class AncienBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AncienBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AncienBoneResourceCrate(int amount)
			: base(CraftResource.AncienBone, 0xA006, amount)
		{
		}

		public AncienBoneResourceCrate(Serial serial)
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


	[Flipable(0xA008, 0xA008)]
	public class IronIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public IronIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public IronIngotResourceCrate(int amount)
			: base(CraftResource.Iron, 0xA008, amount)
		{
		}

		public IronIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class BronzeIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public BronzeIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public BronzeIngotResourceCrate(int amount)
			: base(CraftResource.Bronze, 0xA008, amount)
		{
		}

		public BronzeIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class CopperIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CopperIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CopperIngotResourceCrate(int amount)
			: base(CraftResource.Copper, 0xA008, amount)
		{
		}

		public CopperIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class SonneIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SonneIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SonneIngotResourceCrate(int amount)
			: base(CraftResource.Sonne, 0xA008, amount)
		{
		}

		public SonneIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class ArgentIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ArgentIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ArgentIngotResourceCrate(int amount)
			: base(CraftResource.Argent, 0xA008, amount)
		{
		}

		public ArgentIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class BorealeIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public BorealeIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public BorealeIngotResourceCrate(int amount)
			: base(CraftResource.Boreale, 0xA008, amount)
		{
		}

		public BorealeIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class ChrysteliarIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ChrysteliarIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ChrysteliarIngotResourceCrate(int amount)
			: base(CraftResource.Chrysteliar, 0xA008, amount)
		{
		}

		public ChrysteliarIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class GlaciasIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public GlaciasIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public GlaciasIngotResourceCrate(int amount)
			: base(CraftResource.Glacias, 0xA008, amount)
		{
		}

		public GlaciasIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class LithiarIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public LithiarIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public LithiarIngotResourceCrate(int amount)
			: base(CraftResource.Lithiar, 0xA008, amount)
		{
		}

		public LithiarIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class AcierIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AcierIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AcierIngotResourceCrate(int amount)
			: base(CraftResource.Acier, 0xA008, amount)
		{
		}

		public AcierIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class DurianIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public DurianIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public DurianIngotResourceCrate(int amount)
			: base(CraftResource.Durian, 0xA008, amount)
		{
		}

		public DurianIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class EquilibrumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public EquilibrumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public EquilibrumIngotResourceCrate(int amount)
			: base(CraftResource.Equilibrum, 0xA008, amount)
		{
		}

		public EquilibrumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class GoldIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public GoldIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public GoldIngotResourceCrate(int amount)
			: base(CraftResource.Gold, 0xA008, amount)
		{
		}

		public GoldIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class JolinarIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public JolinarIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public JolinarIngotResourceCrate(int amount)
			: base(CraftResource.Jolinar, 0xA008, amount)
		{
		}

		public JolinarIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class JusticiumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public JusticiumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public JusticiumIngotResourceCrate(int amount)
			: base(CraftResource.Justicium, 0xA008, amount)
		{
		}

		public JusticiumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class AbyssiumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public AbyssiumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public AbyssiumIngotResourceCrate(int amount)
			: base(CraftResource.Abyssium, 0xA008, amount)
		{
		}

		public AbyssiumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class BloodiriumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public BloodiriumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public BloodiriumIngotResourceCrate(int amount)
			: base(CraftResource.Bloodirium, 0xA008, amount)
		{
		}

		public BloodiriumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class HerbrositeIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public HerbrositeIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public HerbrositeIngotResourceCrate(int amount)
			: base(CraftResource.Herbrosite, 0xA008, amount)
		{
		}

		public HerbrositeIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class KhandariumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public KhandariumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public KhandariumIngotResourceCrate(int amount)
			: base(CraftResource.Khandarium, 0xA008, amount)
		{
		}

		public KhandariumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class MytherilIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public MytherilIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public MytherilIngotResourceCrate(int amount)
			: base(CraftResource.Mytheril, 0xA008, amount)
		{
		}

		public MytherilIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class SombralirIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SombralirIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SombralirIngotResourceCrate(int amount)
			: base(CraftResource.Sombralir, 0xA008, amount)
		{
		}

		public SombralirIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class DraconyrIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public DraconyrIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public DraconyrIngotResourceCrate(int amount)
			: base(CraftResource.Draconyr, 0xA008, amount)
		{
		}

		public DraconyrIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class HeptazionIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public HeptazionIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public HeptazionIngotResourceCrate(int amount)
			: base(CraftResource.Heptazion, 0xA008, amount)
		{
		}

		public HeptazionIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class OceanisIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public OceanisIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public OceanisIngotResourceCrate(int amount)
			: base(CraftResource.Oceanis, 0xA008, amount)
		{
		}

		public OceanisIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class BraziumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public BraziumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public BraziumIngotResourceCrate(int amount)
			: base(CraftResource.Brazium, 0xA008, amount)
		{
		}

		public BraziumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class LuneriumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public LuneriumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public LuneriumIngotResourceCrate(int amount)
			: base(CraftResource.Lunerium, 0xA008, amount)
		{
		}

		public LuneriumIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class MarinarIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public MarinarIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public MarinarIngotResourceCrate(int amount)
			: base(CraftResource.Marinar, 0xA008, amount)
		{
		}

		public MarinarIngotResourceCrate(Serial serial)
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

	[Flipable(0xA008, 0xA008)]
	public class NostalgiumIngotResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public NostalgiumIngotResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public NostalgiumIngotResourceCrate(int amount)
			: base(CraftResource.Nostalgium, 0xA008, amount)
		{
		}

		public NostalgiumIngotResourceCrate(Serial serial)
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