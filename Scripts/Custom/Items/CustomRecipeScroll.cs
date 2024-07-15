using System;
using Server;
using Server.Items;

public class CustomRecipeScrolls
{
	public class EpauletteDoreeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public EpauletteDoreeRecipeScroll() : base(10001) { }
		public EpauletteDoreeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class DiademeFeuilleOrRecipeScroll : RecipeScroll
	{
		[Constructable]
		public DiademeFeuilleOrRecipeScroll() : base(10002) { }
		public DiademeFeuilleOrRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class TiareRecipeScroll : RecipeScroll
	{
		[Constructable]
		public TiareRecipeScroll() : base(10003) { }
		public TiareRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class MenotteDoreeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public MenotteDoreeRecipeScroll() : base(10004) { }
		public MenotteDoreeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class TerMurStyleCandelabraRecipeScroll : RecipeScroll
	{
		[Constructable]
		public TerMurStyleCandelabraRecipeScroll() : base(10005) { }
		public TerMurStyleCandelabraRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class Jupe8RecipeScroll : RecipeScroll
	{
		[Constructable]
		public Jupe8RecipeScroll() : base(20001) { }
		public Jupe8RecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class Jupe10RecipeScroll : RecipeScroll
	{
		[Constructable]
		public Jupe10RecipeScroll() : base(20002) { }
		public Jupe10RecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ManteauDoreRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ManteauDoreRecipeScroll() : base(20003) { }
		public ManteauDoreRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ManteauVoyageurRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ManteauVoyageurRecipeScroll() : base(20004) { }
		public ManteauVoyageurRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class RobeNimRecipeScroll : RecipeScroll
	{
		[Constructable]
		public RobeNimRecipeScroll() : base(20005) { }
		public RobeNimRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class RobeBleudecolteRecipeScroll : RecipeScroll
	{
		[Constructable]
		public RobeBleudecolteRecipeScroll() : base(20006) { }
		public RobeBleudecolteRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class Pantalon1RecipeScroll : RecipeScroll
	{
		[Constructable]
		public Pantalon1RecipeScroll() : base(20007) { }
		public Pantalon1RecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class Pantalon3RecipeScroll : RecipeScroll
	{
		[Constructable]
		public Pantalon3RecipeScroll() : base(20008) { }
		public Pantalon3RecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CapePaonRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CapePaonRecipeScroll() : base(20009) { }
		public CapePaonRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CacheOeil3RecipeScroll : RecipeScroll
	{
		[Constructable]
		public CacheOeil3RecipeScroll() : base(20010) { }
		public CacheOeil3RecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class PeauOursRecipeScroll : RecipeScroll
	{
		[Constructable]
		public PeauOursRecipeScroll() : base(20011) { }
		public PeauOursRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class PeauOursPolaireRecipeScroll : RecipeScroll
	{
		[Constructable]
		public PeauOursPolaireRecipeScroll() : base(20012) { }
		public PeauOursPolaireRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class FourreauDoreeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public FourreauDoreeRecipeScroll() : base(20013) { }
		public FourreauDoreeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class GlassblowingBookRecipeScroll : RecipeScroll
	{
		[Constructable]
		public GlassblowingBookRecipeScroll() : base(30001) { }
		public GlassblowingBookRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SandMiningBookRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SandMiningBookRecipeScroll() : base(30002) { }
		public SandMiningBookRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class StoneMiningBookRecipeScroll : RecipeScroll
	{
		[Constructable]
		public StoneMiningBookRecipeScroll() : base(30003) { }
		public StoneMiningBookRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class MasonryBookRecipeScroll : RecipeScroll
	{
		[Constructable]
		public MasonryBookRecipeScroll() : base(30004) { }
		public MasonryBookRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class GemMiningBookRecipeScroll : RecipeScroll
	{
		[Constructable]
		public GemMiningBookRecipeScroll() : base(30005) { }
		public GemMiningBookRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class HitsMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public HitsMaxBuffFoodRecipeScroll() : base(40001) { }
		public HitsMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class GreaterHitsMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public GreaterHitsMaxBuffFoodRecipeScroll() : base(40002) { }
		public GreaterHitsMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class StamMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public StamMaxBuffFoodRecipeScroll() : base(40003) { }
		public StamMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class GreaterStamMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public GreaterStamMaxBuffFoodRecipeScroll() : base(40004) { }
		public GreaterStamMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ManaMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ManaMaxBuffFoodRecipeScroll() : base(40005) { }
		public ManaMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class GreaterManaMaxBuffFoodRecipeScroll : RecipeScroll
	{
		[Constructable]
		public GreaterManaMaxBuffFoodRecipeScroll() : base(40006) { }
		public GreaterManaMaxBuffFoodRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CoffreMaritimeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CoffreMaritimeRecipeScroll() : base(50001) { }
		public CoffreMaritimeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }

	}

	public class FinishedWoodenChestRecipeScroll : RecipeScroll
	{
		[Constructable]
		public FinishedWoodenChestRecipeScroll() : base(50002) { }
		public FinishedWoodenChestRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ChickenCoopRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ChickenCoopRecipeScroll() : base(50003) { }
		public ChickenCoopRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class PoteauChaineRecipeScroll : RecipeScroll
	{
		[Constructable]
		public PoteauChaineRecipeScroll() : base(50004) { }
		public PoteauChaineRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class MountedDreadHornRecipeScroll : RecipeScroll
	{
		[Constructable]
		public MountedDreadHornRecipeScroll() : base(50005) { }
		public MountedDreadHornRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class FoliereRecipeScroll : RecipeScroll
	{
		[Constructable]
		public FoliereRecipeScroll() : base(60001) { }
		public FoliereRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CompositeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CompositeRecipeScroll() : base(60002) { }
		public CompositeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class PieuseRecipeScroll : RecipeScroll
	{
		[Constructable]
		public PieuseRecipeScroll() : base(60003) { }
		public PieuseRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ArbaviveRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ArbaviveRecipeScroll() : base(60004) { }
		public ArbaviveRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class LumitraitRecipeScroll : RecipeScroll
	{
		[Constructable]
		public LumitraitRecipeScroll() : base(60005) { }
		public LumitraitRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SuperiorRefreshPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SuperiorRefreshPotionRecipeScroll() : base(70001) { }
		public SuperiorRefreshPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SuperiorHealPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SuperiorHealPotionRecipeScroll() : base(70002) { }
		public SuperiorHealPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SuperiorCurePotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SuperiorCurePotionRecipeScroll() : base(70003) { }
		public SuperiorCurePotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SuperiorAgilityPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SuperiorAgilityPotionRecipeScroll() : base(70004) { }
		public SuperiorAgilityPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class SuperiorStrengthPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public SuperiorStrengthPotionRecipeScroll() : base(70005) { }
		public SuperiorStrengthPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class InvisibilityPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public InvisibilityPotionRecipeScroll() : base(70006) { }
		public InvisibilityPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class PetBondingPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public PetBondingPotionRecipeScroll() : base(70007) { }
		public PetBondingPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class AutoResPotionRecipeScroll : RecipeScroll
	{
		[Constructable]
		public AutoResPotionRecipeScroll() : base(70008) { }
		public AutoResPotionRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class ForgeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public ForgeRecipeScroll() : base(80011) { }
		public ForgeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class EnclumeRecipeScroll : RecipeScroll
	{
		[Constructable]
		public EnclumeRecipeScroll() : base(80010) { }
		public EnclumeRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CoffreFortRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CoffreFortRecipeScroll() : base(80003) { }
		public CoffreFortRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CoffreMetalVisqueuxRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CoffreMetalVisqueuxRecipeScroll() : base(80004) { }
		public CoffreMetalVisqueuxRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CoffreMetalRouilleRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CoffreMetalRouilleRecipeScroll() : base(80005) { }
		public CoffreMetalRouilleRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}

	public class CoffreMetalDoreRecipeScroll : RecipeScroll
	{
		[Constructable]
		public CoffreMetalDoreRecipeScroll() : base(80006) { }
		public CoffreMetalDoreRecipeScroll(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); reader.ReadInt(); }
	}
}