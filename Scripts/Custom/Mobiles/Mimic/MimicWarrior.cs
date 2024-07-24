using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un mimic")]
    public class MimicWarrior : BaseCreature
    {

        public DateTime LastFreeze;
        [Constructable]
        public MimicWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Un Mimic";
            Body = 86;
            BaseSoundID = 634;

            SetStr(150, 320);
            SetDex(94, 190);
            SetInt(64, 160);

            SetHits(242, 366);
            SetMana(0);

            SetDamage(5, 11);

            SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 50, 75);
			SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 25, 35);

			SetSkill(SkillName.EvalInt, 85.1, 100.0);
			SetSkill(SkillName.Swords, 85.1, 100.0);
			SetSkill(SkillName.MagicResist, 75.0, 97.5);
			SetSkill(SkillName.Tactics, 65.0, 87.5);
			SetSkill(SkillName.Wrestling, 85.1, 100.0);

			Fame = 4500;
            Karma = -4500;

        }

        public MimicWarrior(Serial serial)
            : base(serial)
        {
        }

      	public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
             double chance = 0.10;


            if (Utility.RandomDouble() < chance)
            {
                to.Freeze(TimeSpan.FromSeconds(3));
                to.Emote("*Le mimic vous paralyse.*");

                 LastFreeze = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(6, 9));
            }

			
			base.AlterMeleeDamageTo(to, ref damage);
		}

        public override Poison PoisonImmune => Poison.Greater;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Regular : Poison.Greater);
	


		public override int Meat => Utility.RandomMinMax(2, 6);
		public override int TreasureMapLevel => Utility.RandomMinMax(1, 2);


        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems);
			AddLoot(LootPack.LootItem<Apple>(3, 5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));


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
