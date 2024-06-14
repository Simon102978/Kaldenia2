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
	public class RegularWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public RegularWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public RegularWoodResourceCrate(int amount)
			: base(CraftResource.RegularWood, 0xA004, amount)
		{
		}

		public RegularWoodResourceCrate(Serial serial)
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
	public class PlainoisWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PlainoisWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PlainoisWoodResourceCrate(int amount)
			: base(CraftResource.PlainoisWood, 0xA004, amount)
		{
		}

		public PlainoisWoodResourceCrate(Serial serial)
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
	public class ForestierWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ForestierWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ForestierWoodResourceCrate(int amount)
			: base(CraftResource.ForestierWood, 0xA004, amount)
		{
		}

		public ForestierWoodResourceCrate(Serial serial)
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
	public class CollinoisWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CollinoisWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CollinoisWoodResourceCrate(int amount)
			: base(CraftResource.CollinoisWood, 0xA004, amount)
		{
		}

		public CollinoisWoodResourceCrate(Serial serial)
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
	public class DesertiqueWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public DesertiqueWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public DesertiqueWoodResourceCrate(int amount)
			: base(CraftResource.DesertiqueWood, 0xA004, amount)
		{
		}

		public DesertiqueWoodResourceCrate(Serial serial)
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
	public class SavanoisWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SavanoisWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SavanoisWoodResourceCrate(int amount)
			: base(CraftResource.SavanoisWood, 0xA004, amount)
		{
		}

		public SavanoisWoodResourceCrate(Serial serial)
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
	public class MontagnardWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public MontagnardWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public MontagnardWoodResourceCrate(int amount)
			: base(CraftResource.MontagnardWood, 0xA004, amount)
		{
		}

		public MontagnardWoodResourceCrate(Serial serial)
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
	public class VolcaniqueWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public VolcaniqueWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public VolcaniqueWoodResourceCrate(int amount)
			: base(CraftResource.VolcaniqueWood, 0xA004, amount)
		{
		}

		public VolcaniqueWoodResourceCrate(Serial serial)
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
	public class TropicauxWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public TropicauxWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public TropicauxWoodResourceCrate(int amount)
			: base(CraftResource.TropicauxWood, 0xA004, amount)
		{
		}

		public TropicauxWoodResourceCrate(Serial serial)
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
	public class ToundroisWoodResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ToundroisWoodResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ToundroisWoodResourceCrate(int amount)
			: base(CraftResource.ToundroisWood, 0xA004, amount)
		{
		}

		public ToundroisWoodResourceCrate(Serial serial)
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
	public class ForestierLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ForestierLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ForestierLeatherResourceCrate(int amount)
			: base(CraftResource.LupusLeather, 0xA009, amount)
		{
		}

		public ForestierLeatherResourceCrate(Serial serial)
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
	public class CollinoisLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CollinoisLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CollinoisLeatherResourceCrate(int amount)
			: base(CraftResource.LupusLeather, 0xA009, amount)
		{
		}

		public CollinoisLeatherResourceCrate(Serial serial)
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
	public class DesertiqueLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public DesertiqueLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public DesertiqueLeatherResourceCrate(int amount)
			: base(CraftResource.ReptilienLeather, 0xA009, amount)
		{
		}

		public DesertiqueLeatherResourceCrate(Serial serial)
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
	public class SavanoisLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SavanoisLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SavanoisLeatherResourceCrate(int amount)
			: base(CraftResource.GeantLeather, 0xA009, amount)
		{
		}

		public SavanoisLeatherResourceCrate(Serial serial)
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
	public class MontagnardLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public MontagnardLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public MontagnardLeatherResourceCrate(int amount)
			: base(CraftResource.OphidienLeather, 0xA009, amount)
		{
		}

		public MontagnardLeatherResourceCrate(Serial serial)
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
	public class VolcaniqueLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public VolcaniqueLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public VolcaniqueLeatherResourceCrate(int amount)
			: base(CraftResource.ArachnideLeather, 0xA009, amount)
		{
		}

		public VolcaniqueLeatherResourceCrate(Serial serial)
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
	public class TropicauxLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public TropicauxLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public TropicauxLeatherResourceCrate(int amount)
			: base(CraftResource.DragoniqueLeather, 0xA009, amount)
		{
		}

		public TropicauxLeatherResourceCrate(Serial serial)
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
	public class ToundroisLeatherResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ToundroisLeatherResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ToundroisLeatherResourceCrate(int amount)
			: base(CraftResource.DemoniaqueLeather, 0xA009, amount)
		{
		}

		public ToundroisLeatherResourceCrate(Serial serial)
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
	public class PlainoisBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public PlainoisBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public PlainoisBoneResourceCrate(int amount)
			: base(CraftResource.RegularBone, 0xA006, amount)
		{
		}

		public PlainoisBoneResourceCrate(Serial serial)
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
	public class ForestierBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ForestierBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ForestierBoneResourceCrate(int amount)
			: base(CraftResource.LupusBone, 0xA006, amount)
		{
		}

		public ForestierBoneResourceCrate(Serial serial)
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
	public class CollinoisBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public CollinoisBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public CollinoisBoneResourceCrate(int amount)
			: base(CraftResource.ReptilienBone, 0xA006, amount)
		{
		}

		public CollinoisBoneResourceCrate(Serial serial)
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
	public class DesertiqueBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public DesertiqueBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public DesertiqueBoneResourceCrate(int amount)
			: base(CraftResource.GeantBone, 0xA006, amount)
		{
		}

		public DesertiqueBoneResourceCrate(Serial serial)
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
	public class SavanoisBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public SavanoisBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public SavanoisBoneResourceCrate(int amount)
			: base(CraftResource.OphidienBone, 0xA006, amount)
		{
		}

		public SavanoisBoneResourceCrate(Serial serial)
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
	public class MontagnardBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public MontagnardBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public MontagnardBoneResourceCrate(int amount)
			: base(CraftResource.ArachnideBone, 0xA006, amount)
		{
		}

		public MontagnardBoneResourceCrate(Serial serial)
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
	public class VolcaniqueBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public VolcaniqueBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public VolcaniqueBoneResourceCrate(int amount)
			: base(CraftResource.DragoniqueBone, 0xA006, amount)
		{
		}

		public VolcaniqueBoneResourceCrate(Serial serial)
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
	public class TropicauxBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public TropicauxBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public TropicauxBoneResourceCrate(int amount)
			: base(CraftResource.DemoniaqueBone, 0xA006, amount)
		{
		}

		public TropicauxBoneResourceCrate(Serial serial)
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
	public class ToundroisBoneResourceCrate : BaseResourceCrate
	{
		[Constructable]
		public ToundroisBoneResourceCrate()
			: this(1)
		{
		}

		[Constructable]
		public ToundroisBoneResourceCrate(int amount)
			: base(CraftResource.AncienBone, 0xA006, amount)
		{
		}

		public ToundroisBoneResourceCrate(Serial serial)
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