using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un scorpion du désert")]
    public class DesertScorpion : BaseCreature
    {
        public override bool CanStealth => true;

        public DateTime LastFreeze;
        [Constructable]
        public DesertScorpion()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "un scorpion du désert";
            Body = 0x2CD;
            BaseSoundID = 397;

            SetStr(73, 115);
            SetDex(76, 95);
            SetInt(16, 30);

            SetHits(50, 63);
            SetMana(0);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 40);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 30.1, 35.0);
            SetSkill(SkillName.Tactics, 60.3, 75.0);
            SetSkill(SkillName.Wrestling, 50.3, 65.0);
            SetSkill(SkillName.Hiding,70.0, 100.0);

            Fame = 2000;
            Karma = -2000;

        }

        public override void GenerateLoot()
        {
             AddLoot(LootPack.Meager);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>(true));
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));

		}
        public override void OnThink()
        {

            if (!Alive || Deleted)
            {
                return;
            }

            if (!Hidden)
            {
                double chance = 0.10;

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



        public override void OnDamagedBySpell(Mobile from)
        {
            RevealingAction();
            base.OnDamagedBySpell(from);
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



		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
             double chance = 0.05;

             if ( 3 < to.GetDistanceToSqrt(this.Location))
             {
                chance += 0.20; 
             }

            if ((Hidden || Utility.RandomDouble() < chance ) && LastFreeze < DateTime.UtcNow )
            {
                to.Freeze(TimeSpan.FromSeconds(3));
                to.Emote("*Le scorpion vous paralyse.*");

                 LastFreeze = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(6, 9));
            }

			
			base.AlterMeleeDamageTo(to, ref damage);
		}


        public override int Meat => Utility.RandomMinMax(2, 3);

		public override int Hides => Utility.RandomMinMax(2, 3);
		public override HideType HideType => HideType.Arachnide;

		public override int Bones => Utility.RandomMinMax(2, 3);
		public override BoneType BoneType => BoneType.Arachnide;
		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Arachnid;
        public override Poison PoisonImmune => Poison.Greater;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Regular : Poison.Greater);

        public DesertScorpion(Serial serial)
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
