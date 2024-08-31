using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une tarenlune")]
    public class Szerah : BaseCreature
    {
        public override bool CanStealth => true;  //Stays Hidden until Combatant in range.
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
        public Szerah()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Szerah";
            Body = 11;
            BaseSoundID = 1170;
            Hue = 1161;
      
            SetStr(281, 305);
            SetDex(191, 215);
            SetInt(286, 310);

            SetHits(400, 600);
            SetStam(36, 45);
            SetDamage(10, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Poison, 80);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 20, 30);



            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 90.0, 100.5);
            SetSkill(SkillName.Wrestling, 90.2, 100.0);
			SetSkill(SkillName.Anatomy, 90.2, 100.0);
            SetSkill(SkillName.MagicResist, 102.8, 117.9);
            SetSkill(SkillName.Hiding, 75.0, 97.5);

            Fame = 5000;
            Karma = -5000;


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
         public override void OnDamage(int amount, Mobile from, bool willKill)
		{
            RevealingAction();
			base.OnDamage(amount, from, willKill);
			
		}

         public override void OnDamagedBySpell(Mobile from)
        {
            RevealingAction();
            base.OnDamagedBySpell(from);
         	
        }

    	public override void OnDeath(Container c)
		{
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }

			base.OnDeath(c);
		

		}
        
        public override void OnThink()
        {

            if (!Alive || Deleted)
            {
                return;
            }

            if (!Hidden)
            {
                double chance = 0.05;

                if (Hits < 20)
                {
                    chance = 0.1;
                }

                if (Poisoned)
                {
                    chance = 0.01;
                }

                if (Utility.RandomDouble() < chance)
                {
                    HideSelf();
                }
                base.OnThink();
            }
        }

        private void HideSelf()
        {
            if (Core.TickCount >= NextSkillTime)
            {
                Effects.SendLocationParticles(
                    EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);

                PlaySound(0x22F);
                Hidden = true;

                UseSkill(SkillName.Hiding);
            }
        }




        public Szerah(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Arachnide;
		public override bool CanAngerOnTame => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.LootItem<SpidersSilk>(8, true));
			AddLoot(LootPack.LootItem<VeninTarenlune>(3, 7));
      		AddLoot(LootPack.LootItem<Items.Gold>(400, 600));
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
