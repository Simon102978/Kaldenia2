using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Kepush")]
	public class KepushChaman : KepushBase
	{

		[Constructable]
		public KepushChaman() : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.4, 0.2)
		{
		
			SpeechHue = Utility.RandomDyedHue();
			Title = "Kepush Chaman";
			Race = Race.GetRace(7);

			if (Female = Utility.RandomBool())
			{
				Body = 0x191;
				Name = NameList.RandomName("savage");

                AddItem(new LeatherSkirt());
                AddItem(new LeatherBustierArms());
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName("savage");
			    AddItem(new LeatherShorts());
			}

			BodyValue = 0x190;
//			Hue = 1281;

			SetStr(171, 200);
			SetDex(126, 145);
			SetInt(200, 265);

			SetHits(103, 120);

			//	SetHits(1200);
			SetMana(600, 800);
			SetDamage(10, 15);

			SetDamageType(ResistanceType.Physical, 50);
			SetDamageType(ResistanceType.Energy, 50);

			SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);

			SetSkill(SkillName.MagicResist, 125, 140);
			SetSkill(SkillName.Tactics, 50, 120);
			SetSkill(SkillName.Wrestling, 80, 130);
			SetSkill(SkillName.Magery, 70, 100);
			SetSkill(SkillName.EvalInt, 70, 100);


            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new TribalMask());


		}

		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);
			
			if (DelayBleeding < DateTime.UtcNow)
			{
				MakeBleeding(from);

				DelayBleeding = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}

            return dam;
        }


		public override TribeType Tribe => TribeType.Kepush;
		public override void GenerateLoot()
		{
			AddLoot(LootPack.Rich);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.MedScrolls);
			AddLoot(LootPack.MageryRegs, 15);
			AddLoot(LootPack.Potions, Utility.RandomMinMax(1, 2));

		}

		public KepushChaman(Serial serial)
			: base(serial)
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