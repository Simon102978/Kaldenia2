using Server.Items;

namespace Server.Mobiles
{

    public class Courtisane : BaseCreature
    {
        [Constructable]
        public Courtisane()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "Une Courtisane";
           	Race = BaseRace.GetRace(Utility.Random(4));

            Body = 0x191;
            Name = NameList.RandomName("female");
            AddItem(new Skirt(Utility.RandomNeutralHue()));
            
            SetStr(86, 100);
            SetDex(81, 95);
            SetInt(61, 75);

            SetDamage(5, 15);

            SetSkill(SkillName.Fencing, 66.0, 97.5);
            SetSkill(SkillName.Macing, 65.0, 87.5);
            SetSkill(SkillName.MagicResist, 25.0, 47.5);
            SetSkill(SkillName.Swords, 65.0, 87.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 15.0, 37.5);
         	SetSkill(SkillName.Musicianship, 65.0, 87.5);
			SetSkill(SkillName.Discordance, 65.0, 87.5);
			SetSkill(SkillName.Provocation, 65.0, 87.5);
			SetSkill(SkillName.Peacemaking, 65.0, 87.5);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt());
            AddItem(new Bandana());

            switch (Utility.Random(7))
            {
                case 0:
                    AddItem(new Longsword());
                    break;
                case 1:
                    AddItem(new Cutlass());
                    break;
                case 2:
                    AddItem(new Broadsword());
                    break;
                case 3:
                    AddItem(new Axe());
                    break;
                case 4:
                    AddItem(new Club());
                    break;
                case 5:
                    AddItem(new Dagger());
                    break;
                case 6:
                    AddItem(new Spear());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public Courtisane(Serial serial)
            : base(serial)
        {
        }

       	public override bool CanDiscord => true;
		public override bool CanPeace => true;
		public override bool CanProvoke => true;


		public override TribeType Tribe => TribeType.Brigand;

		public override bool ClickTitle => false;
        public override bool AlwaysMurderer => true;

        public override bool ShowFameTitle => false;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

//            if (Utility.RandomDouble() < 0.75)
  //              c.DropItem(new SeveredHumanEars());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average,3);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 4));
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