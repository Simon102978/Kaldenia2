using Server.Items;

namespace Server.Mobiles
{
	[CorpseName("le corps d'un dragon")]
	public class Dragon : BaseCreature
	{
		[Constructable]
		public Dragon()
			: base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "un dragon";
			Body = Utility.RandomList(12, 59);
			BaseSoundID = 362;

			SetStr(796, 825);
			SetDex(86, 105);
			SetInt(436, 475);

			SetHits(478, 495);

			SetDamage(16, 22);

			SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 55, 65);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 30, 40);
			SetResistance(ResistanceType.Poison, 25, 35);
			SetResistance(ResistanceType.Energy, 35, 45);

			SetSkill(SkillName.EvalInt, 30.1, 40.0);
			SetSkill(SkillName.Magery, 30.1, 40.0);
			SetSkill(SkillName.MagicResist, 99.1, 100.0);
			SetSkill(SkillName.Tactics, 97.6, 100.0);
			SetSkill(SkillName.Wrestling, 90.1, 92.5);

			Fame = 15000;
			Karma = -15000;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 95.9;

			SetSpecialAbility(SpecialAbility.DragonBreath);
		}

		public Dragon(Serial serial)
			: base(serial)
		{
		}

		public override bool ReacquireOnMovement => !Controlled;
		public override bool AutoDispel => !Controlled;
		public override int TreasureMapLevel => 4;
		public override int Meat => Utility.RandomMinMax(8, 16);

		public override int DragonBlood => 8;
		public override int Hides => Utility.RandomMinMax(8, 16);
		public override HideType HideType => HideType.Dragonique;
		public override int Bones => Utility.RandomMinMax(8, 16);
		public override BoneType BoneType => BoneType.Dragonique;

		public override FoodType FavoriteFood => FoodType.Meat;
		public override bool CanAngerOnTame => true;
		public override bool CanFly => true;

		public override void OnAfterTame(Mobile tamer)
		{
			base.OnAfterTame(tamer);

			// Diviser les stats par 3
			//RawStr /= 3;
			//RawDex /= 3;
			//RawInt /= 3;
			//Hits /= 3;

			Body = 716;

			Name = "un bébé dragon";
			Hue = 1999;

		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.FilthyRich, 2);
			AddLoot(LootPack.LootItem<Items.GemmeFeu>(), (double)10);
			AddLoot(LootPack.Others, Utility.RandomMinMax(7, 14));
		}

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteDragon>(), Utility.RandomMinMax(2, 4));
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
