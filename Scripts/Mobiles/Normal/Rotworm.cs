using Server.Engines.Quests;
using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un vers")]
    [TypeAlias("Server.Mobiles.RotWorm")]
    public class Rotworm : BaseCreature
    {
        [Constructable]
        public Rotworm()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.25, 0.5)
        {
            Name = "un vers";
            Body = 732;

            SetStr(200, 300);
            SetDex(80);
            SetInt(15, 20);

            SetHits(200, 250);
            SetStam(50);

            SetDamage(1, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 65, 75);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 25.0);
            SetSkill(SkillName.Tactics, 25.0);
            SetSkill(SkillName.Wrestling, 50.0);

            Fame = 500;
            Karma = -500;

            SetSpecialAbility(SpecialAbility.BloodDisease);
        }

        public Rotworm(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound() { return 0x62D; }
        public override int GetIdleSound() { return 0x62D; }
        public override int GetAttackSound() { return 0x62A; }
        public override int GetHurtSound() { return 0x62C; }
        public override int GetDeathSound() { return 0x62B; }

        public override int Meat => 2;
        public override MeatType MeatType => MeatType.Rotworm;
        public override FoodType FavoriteFood => FoodType.Fish;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.BodyPartsAndBones);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

		}

		public override void OnKilledBy(Mobile mob)
        {
            base.OnKilledBy(mob);

            if (mob is PlayerMobile && 0.2 > Utility.RandomDouble())
            {
                PlayerMobile pm = mob as PlayerMobile;

                if (QuestHelper.HasQuest<Missing>(pm))
                {
                    // As the rotworm dies, you find and pickup a scroll case. Inside the scroll case is parchment. The scroll case crumbles to dust.
                    pm.SendLocalizedMessage(1095146);

                    pm.AddToBackpack(new ArielHavenWritofMembership());
                }
            }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            CandlewoodTorch torch = m.FindItemOnLayer(Layer.TwoHanded) as CandlewoodTorch;

            if (torch != null && torch.Burning)
            {
                ForceFleeUntil = DateTime.UtcNow + TimeSpan.FromSeconds(5.0);
            }
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
