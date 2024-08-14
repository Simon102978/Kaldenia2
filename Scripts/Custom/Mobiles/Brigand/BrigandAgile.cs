using System;
using Server.Items;

namespace Server.Mobiles
{

    public class BrigandAgile : BaseCreature
    {
        [Constructable]
        public BrigandAgile()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "Un Brigand";
           	Race = BaseRace.GetRace(Utility.Random(4));

			if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new Skirt(Utility.RandomNeutralHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            SetStr(200, 300);
            SetDex(81, 95);
            SetInt(61, 75);

            SetDamage(10, 15);

            SetSkill(SkillName.Fencing, 66.0, 97.5);
            SetSkill(SkillName.Macing, 65.0, 87.5);
            SetSkill(SkillName.MagicResist, 25.0, 47.5);
            SetSkill(SkillName.Swords, 65.0, 87.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 15.0, 37.5);

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

        public BrigandAgile(Serial serial)
            : base(serial)
        {
        }

		public override TribeType Tribe => TribeType.Brigand;

		public override bool ClickTitle => false;
        public override bool AlwaysMurderer => true;

        public override bool ShowFameTitle => false;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

            if (Utility.Random(10) < 2)
            {
                  Item toDisarm = to.FindItemOnLayer(Layer.OneHanded);

                    if (toDisarm == null || !toDisarm.Movable)
                        toDisarm = to.FindItemOnLayer(Layer.TwoHanded);

                    Container pack = to.Backpack;

                    if (pack == null || (toDisarm != null && !toDisarm.Movable))
                    {
                        to.SendLocalizedMessage(1004001); // You cannot disarm your opponent.
                    }
                    else if (toDisarm == null || toDisarm is BaseShield)
                    {
                        to.SendLocalizedMessage(1060849); // Your target is already unarmed!
                    }
                
                SendLocalizedMessage(1060092); // You disarm their weapon!
                to.SendLocalizedMessage(1060093); // Your weapon has been disarmed!

                to.PlaySound(0x3B9);
                to.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);

                pack.DropItem(toDisarm);

                BuffInfo.AddBuff(to, new BuffInfo(BuffIcon.NoRearm, 1075637, TimeSpan.FromSeconds(5.0), to));

                BaseWeapon.BlockEquip(to, TimeSpan.FromSeconds(5.0));

                if (to is BaseCreature)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(5.0) + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 10)), () =>
                    {
                        if (toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(to.Backpack))
                            to.EquipItem(toDisarm);
                    });
                }

                Disarm.AddImmunity(to, TimeSpan.FromSeconds(10));

            }
      

			base.AlterMeleeDamageTo(to, ref damage);
		}

        public override Poison HitPoison => Poison.Deadly;


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