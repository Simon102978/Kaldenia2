using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("le corps d'un {0}")]
	public class wolfspider : BaseCreature
	{
		[Constructable]
		public wolfspider() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "un lezard de pierre";
			Body = 0x2DE;
			BaseSoundID = 0;

			SetStr(279);
			SetDex(85);
			SetInt(61);

			SetHits(160);
			SetMana(51);
			SetStam(80);

			SetDamage(6, 24);

			SetDamageType(ResistanceType.Physical, 100);
			SetDamageType(ResistanceType.Fire, 0);
			SetDamageType(ResistanceType.Cold, 0);
			SetDamageType(ResistanceType.Poison, 0);
			SetDamageType(ResistanceType.Energy, 0);

			SetResistance(ResistanceType.Physical, 55);
			SetResistance(ResistanceType.Fire, 20);
			SetResistance(ResistanceType.Cold, 19);
			SetResistance(ResistanceType.Poison, 40);
			SetResistance(ResistanceType.Energy, 35);

			SetSkill(SkillName.MagicResist, 90,1);
			SetSkill(SkillName.Tactics, 87,5);
			SetSkill(SkillName.Wrestling, 83,1);

			Fame = 0;
			Karma = 0;

			VirtualArmor = 0;
			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 65.1;
		}

		public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs;

		public wolfspider(Serial serial) : base(serial)
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
