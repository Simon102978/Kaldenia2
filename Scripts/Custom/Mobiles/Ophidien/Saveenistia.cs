using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un chevalier ophidien")]
    public class Saveenistia : BaseCreature
    {
		public DateTime LastFreeze;
        private WallControlerStone m_Stone;

        [CommandProperty(AccessLevel.GameMaster)]
        public WallControlerStone Stone
        {
            get
            {
                return m_Stone;
            }
            set
            {
                m_Stone = value;
                
               
            }
        }


        [Constructable]
        public Saveenistia()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = "Saveenistia";
            Title = "Championne Ophidienne";
            Body = 86;
            BaseSoundID = 634;
            Hue = 1159;

            SetStr(417, 595);
            SetDex(166, 175);
            SetInt(46, 70);


            SetHits(642, 946);
            SetMana(0);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 75);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 90, 100);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 150.0);

            Fame = 5000;
            Karma = -5000;
            AddItem(new BardicheOphidian());
        }

         protected override void OnCreate()
        {          
            base.OnCreate();
            CheckWallControler();
        }

        public void CheckWallControler()
        {

           IPooledEnumerable eable = Map.GetItemsInRange(this.Location, 10);

            foreach (Item item in eable)
            {
                if (item is WallControlerStone wc)
                {
                    m_Stone = wc;
                    break;
                }
            }
            eable.Free();

            if (m_Stone != null)
            {
                m_Stone.MobActif = true;
            }

        }

        public override void OnDeath(Container c)
        {
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }


            base.OnDeath(c);
        }

        public Saveenistia(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 2);

		public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;
        public override bool Unprovokable => true;

        public override bool CanBeParagon => false;

        public override TribeType Tribe => TribeType.Ophidian;

        public override bool CanFlee => false;

		public override int Hides => Utility.RandomMinMax(1, 3);
		public override int Bones => Utility.RandomMinMax(1, 3);
		public override HideType HideType => HideType.Ophidien;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));

		}
        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

			   double chance = 0.25;

  			if ((Utility.RandomDouble() < chance ) && LastFreeze < DateTime.UtcNow )
            {
                from.Freeze(TimeSpan.FromSeconds(8));
                from.Emote("*Saveenistia te maudit.*");

                 LastFreeze = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 12));
            }

            return dam;
        }


		
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 4);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>());
			AddLoot(LootPack.Others, Utility.RandomMinMax(2, 4));
            AddLoot(LootPack.LootItem<Items.Gold>(200,500));

		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(m_Stone);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_Stone = (WallControlerStone)reader.ReadItem();
        }
    }
}
