using Server.Commands;
using Server.Items;

namespace Server.Mobiles
{
    public class KepushArcher : KepushBase
	{
        [Constructable]
        public KepushArcher()
		   : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.05, 0.2)
		{
            SpeechHue = Utility.RandomDyedHue();
			Race = BaseRace.GetRace(7);

		
			Title = "Kepush barde";

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
    //        Title = "the bard";
            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

          	SetHits(251, 350);

            SetStr(126, 225);
            SetDex(81, 95);
            SetInt(151, 165);

            SetDamage(10, 20);

            SetSkill(SkillName.Anatomy, 105.0, 120.0);
            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 115.0, 130.0);
            SetSkill(SkillName.Archery, 95.0, 120.0);
            SetSkill(SkillName.Magery, 95.0, 120.0);

            Fame = 1000;
            Karma = 1000;

            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new TribalMask());


			
    
			AddItem(new Bow());


		}

        public override bool AutoDispel => true;
        public override double AutoDispelChance => 0.3;



		public override void GenerateLoot()
        {
            AddLoot(LootPack.RandomLootItem(new System.Type[] { typeof(Harp), typeof(Lute), typeof(Drums), typeof(Tambourine) }));
            AddLoot(LootPack.LootItem<Longsword>(true));
            AddLoot(LootPack.LootItem<Bow>(true));
            AddLoot(LootPack.LootItem<Arrow>(100, true));
			AddLoot(LootPack.Average);
			//      AddLoot(LootPack.LootGold(10, 50));
		}

        public KepushArcher(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle => false;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);// version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
