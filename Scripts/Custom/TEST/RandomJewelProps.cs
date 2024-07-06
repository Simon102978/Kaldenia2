using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RandomMagicJewelry : BaseJewel
	{
		private static Type[] JewelryTypes = new Type[]
		{
			typeof(GoldBracelet),
			typeof(SilverBracelet),
			typeof(GoldRing),
			typeof(SilverRing),
			typeof(GoldNecklace),
			typeof(SilverNecklace)
		};

		private static SkillName[] AllSkills = new SkillName[]
		{
			SkillName.Alchemy, SkillName.Anatomy, SkillName.AnimalLore,
			SkillName.Parry, SkillName.Botanique, SkillName.Blacksmith,
			SkillName.Concentration, SkillName.Peacemaking, SkillName.Camping,
			SkillName.Carpentry, SkillName.Cartography, SkillName.Cooking,
			SkillName.DetectHidden, SkillName.Discordance, SkillName.EvalInt,
			SkillName.Healing, SkillName.Fishing, SkillName.Provocation,
			SkillName.Inscribe, SkillName.Lockpicking, SkillName.Magery,
			SkillName.MagicResist, SkillName.Tactics, SkillName.Snooping,
			SkillName.Musicianship, SkillName.Poisoning, SkillName.Archery,
			SkillName.SpiritSpeak, SkillName.Stealing, SkillName.Tailoring,
			SkillName.AnimalTaming, SkillName.Tinkering, SkillName.Veterinary,
			SkillName.Swords, SkillName.Macing, SkillName.Fencing,
			SkillName.Wrestling, SkillName.Lumberjacking, SkillName.Mining,
			SkillName.Meditation, SkillName.EvalInt, SkillName.RemoveTrap,
			SkillName.Necromancy, SkillName.Focus
		};

		[Constructable]
		public RandomMagicJewelry() : base(0x1088, Layer.Bracelet)
		{
			Type randomType = JewelryTypes[Utility.Random(JewelryTypes.Length)];
			Item jewelry = (Item)Activator.CreateInstance(randomType);

			jewelry.Name = "Random Jewelry";
			jewelry.Hue = Utility.RandomDyedHue();

			if (jewelry is BaseJewel baseJewel)
			{
				AssignRandomProperties(baseJewel);
			}

			Movable = true;
			ItemID = jewelry.ItemID;
			Hue = jewelry.Hue;
			Name = jewelry.Name;

			if (jewelry is BaseJewel jewel)
			{
				// Copier les attributs de l'objet baseJewel vers l'objet jewel
				Attributes = new AosAttributes(jewel);
				SkillBonuses = new AosSkillBonuses(jewel);
			}

			jewelry.Delete();
		}

		private void AssignRandomProperties(BaseJewel jewelry)
		{
			int prop1 = Utility.Random(7);
			int prop2;
			do
			{
				prop2 = Utility.Random(7);
			} while (prop2 == prop1);

			AddRandomProperty(jewelry, prop1);
			AddRandomProperty(jewelry, prop2);
		}

		private void AddRandomProperty(BaseJewel jewelry, int prop)
		{
			switch (prop)
			{
				case 0:
					jewelry.Attributes.BonusDex = Utility.Random(1, 10);
					break;
				case 1:
					jewelry.Attributes.BonusInt = Utility.Random(1, 10);
					break;
				case 2:
					jewelry.Attributes.BonusStr = Utility.Random(1, 10);
					break;
				case 3:
					AddRandomSkillBonus(jewelry);
					break;
				case 4:
					jewelry.Attributes.BonusMana = Utility.Random(1, 10);
					break;
				case 5:
					jewelry.Attributes.BonusStam = Utility.Random(1, 10);
					break;
				case 6:
					jewelry.Attributes.BonusHits = Utility.Random(1, 10);
					break;
			}
		}

		private void AddRandomSkillBonus(BaseJewel jewelry)
		{
			int skillIndex = Utility.Random(AllSkills.Length);
			SkillName randomSkill = AllSkills[skillIndex];
			double skillValue = Utility.Random(1, 10);
			jewelry.SkillBonuses.SetValues(0, randomSkill, skillValue);
		}

		

		public RandomMagicJewelry(Serial serial) : base(serial)
		{
		}

		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
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
