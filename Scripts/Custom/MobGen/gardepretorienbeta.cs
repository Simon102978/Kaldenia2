using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("le corps d'un garde pretorien")]
	public class gardepretorienbeta : BaseCreature
	{
		[Constructable]
		public gardepretorienbeta() : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4)
		{
			Name = "Gaius Lupus";
			Body = 0x190;
			BaseSoundID = 0;
			Hue = 1017;

			SetStr(157);
			SetDex(180);
			SetInt(180);

			SetHits(157);
			SetMana(180);
			SetStam(180);

			SetDamage(4, 4);

			SetDamageType(ResistanceType.Physical, 100);
			SetDamageType(ResistanceType.Fire, 0);
			SetDamageType(ResistanceType.Cold, 0);
			SetDamageType(ResistanceType.Poison, 0);
			SetDamageType(ResistanceType.Energy, 0);

			SetResistance(ResistanceType.Physical, 100);
			SetResistance(ResistanceType.Fire, 100);
			SetResistance(ResistanceType.Cold, 100);
			SetResistance(ResistanceType.Poison, 100);
			SetResistance(ResistanceType.Energy, 100);

			SetSkill(SkillName.MagicResist, 144);
			SetSkill(SkillName.Tactics, 144);
			SetSkill(SkillName.Wrestling, 144);

			Fame = 0;
			Karma = 0;

			VirtualArmor = 0;

			// Équipement
			AddItem(new Backpack());
			AddItem(new Backpack
			{
				Layer = Layer.ShopBuy,
			});
			AddItem(new Backpack
			{
				Layer = Layer.ShopResale,
			});
			AddItem(new CorpsKorain
			{
				Name = "Korain",
				Hue = 1017,
				LootType = LootType.Blessed,
				Layer = Layer.Shirt,
			});
			
			AddItem(new Sandals
			{
				Name = "Sandales",
				Layer = Layer.Shoes,
			});
			AddItem(new TogeKoraine
			{
				Name = "Toge Évasée",
				Hue = 1461,
				LootType = LootType.Blessed,
				Layer = Layer.MiddleTorso,
			});
			AddItem(new EpeeCourte
			{
				Name = "Épée Courte",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.FirstValid,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new Pavois
			{
				Name = "Pavois",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.TwoHanded,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new BrassardEmbellit
			{
				Name = "Brassard de demi-plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Arms,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new CasqueKorain
			{
				Name = "Casque Korain",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Helm,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new KnightsPlateGorget
			{
				Name = "Gorget Plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Neck,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new GantsEmbellit
			{
				Name = "Gants de demi-plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Gloves,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new CapeOfCourage
			{
				Name = "Cape de la Garde pretorienne",
				Hue = 1461,
				LootType = LootType.Blessed,
				Layer = Layer.Cloak,
				Quality = ItemQuality.Legendary,
			});
			AddItem(new Kilt2
			{
				Name = "Kilt à Bandouillère",
				Hue = 2354,
				LootType = LootType.Blessed,
				Layer = Layer.Pants,
			});
			AddItem(new Earrings
			{
				Name = "Boucles d'oreilles pendantes",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Earrings,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});

			Race = Race.Human;
		}

		public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs;

		public gardepretorienbeta(Serial serial) : base(serial)
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
