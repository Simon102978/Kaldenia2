using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("un corps de shaman ssins")]
    public class RatmanMage : BaseCreature
    {
        [Constructable]
        public RatmanMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un shaman ssins";
            Body = 0x8F;
            BaseSoundID = 437;

            SetStr(146, 180);
            SetDex(101, 130);
            SetInt(186, 210);

            SetHits(88, 108);

            SetDamage(7, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 45);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 90.0);
            SetSkill(SkillName.Tactics, 50.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 7500;
            Karma = -7500;
        }

        public RatmanMage(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 2;
        public override int Meat => Utility.RandomMinMax(1, 2);

		public override int Hides => Utility.RandomMinMax(3, 7);

		public override HideType HideType => HideType.Regular;

		public override int Bones => Utility.RandomMinMax(3, 7);
		public override BoneType BoneType => BoneType.Regular;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.MageryRegs, 6);
            AddLoot(LootPack.Statue);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.LootItem<SsinsEar>(1, 2, true));



			AddLoot(LootPack.RandomLootItem(new Type[] { typeof(AnimateDeadScroll), typeof(BloodOathScroll), typeof(CorpseSkinScroll),
                typeof(CurseWeaponScroll), typeof(EvilOmenScroll), typeof(HorrificBeastScroll), typeof(MindRotScroll),
                typeof(CloseWoundsScroll), typeof(WraithFormScroll), typeof(PoisonStrikeScroll) }, 20.0, 1, false, true));

			AddLoot(LootPack.RandomLootItem(new System.Type[] { typeof(CheeseWedge), typeof(CheeseSlice), typeof(CheeseWheel) }, 25.0, 2, false, true));
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
