using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Legionaire : BaseCreature
	{
		public Legionaire() : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			Name = "Sahale";
			Body = 0x190;
			BaseSoundID = 0;
			Hue = 33812;

			SetStr(91);
			SetDex(91);
			SetInt(50);

			SetHits(91);
			SetMana(50);
			SetStam(91);

			SetDamage(7, 14);

			SetDamageType(ResistanceType.Physical, 100);
			SetDamageType(ResistanceType.Fire, 0);
			SetDamageType(ResistanceType.Cold, 0);
			SetDamageType(ResistanceType.Poison, 0);
			SetDamageType(ResistanceType.Energy, 0);

			SetResistance(ResistanceType.Physical, 62);
			SetResistance(ResistanceType.Fire, 33);
			SetResistance(ResistanceType.Cold, 9);
			SetResistance(ResistanceType.Poison, 37);
			SetResistance(ResistanceType.Energy, 15);

			SetSkill(SkillName.MagicResist, 0);
			SetSkill(SkillName.Tactics, 53,3);
			SetSkill(SkillName.Wrestling, 43,4);

			Fame = 100;
			Karma = 100;

			VirtualArmor = 0;

			// Équipement
			AddItem(new CorpsKalois
			{
				Name = "Kalois",
				Hue = 1819,
				LootType = LootType.Blessed,
				Layer = Layer.Shirt,
			});
			AddItem(new Pavois2
			{
				Name = "Pavois Décoratif",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.TwoHanded,
				Quality = ItemQuality.Epic,
				Resource = CraftResource.Acier,
			});
			AddItem(new VikingSword
			{
				LootType = LootType.Blessed,
				Layer = Layer.FirstValid,
				Quality = ItemQuality.Epic,
				Resource = CraftResource.Iron,
			});
			AddItem(new Cocarde
			{
				Name = "Cocarde",
				Hue = 2252,
				LootType = LootType.Blessed,
				Layer = Layer.Waist,
			});
			AddItem(new Ceinture
			{
				Name = "Ceinture boucle ronde",
				LootType = LootType.Blessed,
				Layer = Layer.Bracelet,
			});
			AddItem(new PlastronMailleRenforce
			{
				Name = "Plastron Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.InnerTorso,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new BrassardMaillerRenforce
			{
				Name = "Brassard Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Arms,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new GantMailleRenforce
			{
				Name = "Gants Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Gloves,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new JambiereMailleRenforce
			{
				Name = "Jambière Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Pants,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new Boots
			{
				Name = "Bottes simples",
				Hue = 1889,
				LootType = LootType.Blessed,
				Layer = Layer.Shoes,
			});
			AddItem(new Cape7
			{
				Name = "Cape brodee d'un aigle d'or",
				Hue = 2252,
				LootType = LootType.Blessed,
				Layer = Layer.Cloak,
			});
			AddItem(new CasqueKorain
			{
				Name = "Casque Korain",
				LootType = LootType.Blessed,
				Layer = Layer.Helm,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Iron,
			});
		}

		public Legionaire(Serial serial) : base(serial)
		{
		}

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
