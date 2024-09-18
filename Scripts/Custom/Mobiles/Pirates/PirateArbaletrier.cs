using System;
using Server.Commands;
using Server.Items;

namespace Server.Mobiles
{
    public class PirateArbaletrier : PirateBase
	{
        public override bool AllowCharge => false;


         [Constructable]
        public PirateArbaletrier()
            : this(0)
        {

           
        }

        [Constructable]
        public PirateArbaletrier(int PirateBoatId)
		   : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.05, 0.2, PirateBoatId)
		{
          	SetHits(351, 550);

            SetStr(226, 325);
            SetDex(201, 305);
            SetInt(151, 165);

            SetDamage(15, 25);

			SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);


            SetSkill(SkillName.Anatomy, 105.0, 120.0);
            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 115.0, 130.0);
            SetSkill(SkillName.Archery, 105.0, 120.0);

            Fame = 1000;
            Karma = 1000;
			
    
			AddItem(new Crossbow());


		}

        public override bool AutoDispel => true;
        public override double AutoDispelChance => 0.3;



	    public override void AlterRangedDamageTo(Mobile to, ref int damage)
		{
            int HalfDmg = damage / 2;

            if (to != null)
            {
                to.Hits -= HalfDmg;
                damage -= HalfDmg;

                double chance = 0.20;

			
                if (Utility.RandomDouble() < chance)
                {
                  to.Paralyze(TimeSpan.FromSeconds(1));
                }

            }

             


			base.AlterRangedDamageTo(to, ref damage);
		}

		public override void GenerateLoot()
        {

            AddLoot(LootPack.LootItem<Longsword>(true));
            AddLoot(LootPack.LootItem<Crossbow>(true));
            AddLoot(LootPack.LootItem<Bolt>(100, true));
            base.GenerateLoot();

		}

        public PirateArbaletrier(Serial serial)
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
