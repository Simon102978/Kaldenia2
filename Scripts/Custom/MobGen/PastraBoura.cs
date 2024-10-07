using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("le corps d'un boura des plaines")]
	public class PastraBoura : BaseCreature
	{
		[Constructable]
		public PastraBoura() : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			Name = "un boura des plaines";
			Body = 0x2CB;
			BaseSoundID = 0;

			SetStr(424);
			SetDex(94);
			SetInt(27);

			SetHits(582);
			SetMana(27);
			SetStam(94);

			SetDamage(20, 25);

			SetDamageType(ResistanceType.Physical, 100);
			SetDamageType(ResistanceType.Fire, 0);
			SetDamageType(ResistanceType.Cold, 0);
			SetDamageType(ResistanceType.Poison, 0);
			SetDamageType(ResistanceType.Energy, 0);

			SetResistance(ResistanceType.Physical, 52);
			SetResistance(ResistanceType.Fire, 39);
			SetResistance(ResistanceType.Cold, 14);
			SetResistance(ResistanceType.Poison, 37);
			SetResistance(ResistanceType.Energy, 40);

			SetSkill(SkillName.MagicResist, 65);
			SetSkill(SkillName.Tactics, 104,5);
			SetSkill(SkillName.Wrestling, 114,1);

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 0;
			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 47.1;
		}

		public override FoodType FavoriteFood => FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs;

		public PastraBoura(Serial serial) : base(serial)
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
