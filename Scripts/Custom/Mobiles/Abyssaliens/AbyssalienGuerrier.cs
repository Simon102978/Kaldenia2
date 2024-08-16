using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un abyssalien")]
    public class AbyssalienGuerrier : BaseCreature
    {
		public DateTime DelayCharge;
		public DateTime TuerSummoneur;

		[Constructable]
        public AbyssalienGuerrier()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Abyssaliens";
            Body = 303;
            BaseSoundID = 357;
            Hue = 1043;


			SetStr(200, 300);
            SetDex(151, 165);
            SetInt(161, 175);

            SetDamage(8, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 30);
            SetResistance(ResistanceType.Cold, 25, 30);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 125.0);
            SetSkill(SkillName.Fencing, 46.0, 77.5);
            SetSkill(SkillName.Macing, 35.0, 57.5);
            SetSkill(SkillName.Poisoning, 60.0, 82.5);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 125.0);
            SetSkill(SkillName.Tactics, 125.0);
            SetSkill(SkillName.Lumberjacking, 125.0);

            Fame = 5000;
            Karma = -5000;

        }

        public AbyssalienGuerrier(Serial serial)
            : base(serial)
        {
        }


        public override int Hides => Utility.RandomMinMax(2, 3);

		public override HideType HideType => HideType.Demoniaque;


		public override int Bones => Utility.RandomMinMax(2, 3);

		public override BoneType BoneType => BoneType.Demoniaque;


		public bool BlockReflect { get; set; }




		public override void OnThink()
		{
			base.OnThink();

			if (Combatant != null)
			{

                    if (Combatant is BaseCreature bc)
                    {
                        AntiSummon();
                    }

					if (!this.InRange(Combatant.Location, 3) && this.InRange(Combatant.Location, 10))
					{
						Charge();
					}
			}
		}

		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow)
			{
					if (Combatant is CustomPlayerMobile cp)
					{

						Emote($"*Effectue une charge vers {cp.Name}*");

						cp.Damage(15);

						cp.Freeze(TimeSpan.FromSeconds(3));

						this.Location = cp.Location;
					}
				

				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}

		public void AntiSummon()
		{
			if (TuerSummoneur < DateTime.UtcNow)
			{
				if (Combatant is BaseCreature bc)
				{

						if (bc.ControlMaster is CustomPlayerMobile cp)
						{
							Combatant = cp;

							Emote($"*Effectue une charge vers {cp.Name}*");

							cp.Damage(15);

							cp.Freeze(TimeSpan.FromSeconds(3));

							this.Location = cp.Location;

						}
					}
				
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}



		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

            if (!BlockReflect && from != null && dam > 0)
            {
                BlockReflect = true;
                AOS.Damage(from, this, dam, 0, 0, 0, 0, 0, 0, 100);
                BlockReflect = false;

                from.PlaySound(0x1F1);
            }

            return dam;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Meager);
			AddLoot(LootPack.Others, Utility.RandomMinMax(5, 6));
			AddLoot(LootPack.LootItem<Items.Gold>(50, 150));

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
