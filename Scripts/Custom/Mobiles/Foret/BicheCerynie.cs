using System;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une biche")]
    public class BicheCerynie : BaseCreature
    {
        public DateTime DelayCharge;
		public DateTime TuerSummoneur;

        [Constructable]
        public BicheCerynie()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "Biche de Cerynie";
            Body = 0xED;

            Hue = 2146;


            SetStr(73, 115);
            SetDex(76, 95);
            SetInt(16, 30);

            SetHits(100, 150);
            SetMana(0);

            SetDamage(7, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 5, 15);
            SetResistance(ResistanceType.Cold, 5);

            SetSkill(SkillName.MagicResist, 30.1, 35.0);
            SetSkill(SkillName.Tactics, 60.3, 75.0);
            SetSkill(SkillName.Wrestling, 70.3, 80.0);

            Fame = 2000;
            Karma = -2000;


        }

        public override void OnThink()
		{
			base.OnThink();

			if (Combatant != null)
			{

					if (!this.InRange(Combatant.Location, 3) && this.InRange(Combatant.Location, 10))
					{
						Charge();
					}
					else
					{
						AntiSummon();
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
					if (bc.Summoned)
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
				}
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}

    	public override void OnDamage(int amount, Mobile from, bool willKill)
		{
            int newAmount = amount;

			if (amount < 5 )
            {
                newAmount = 0;
            }
            else
            {
                newAmount = amount - 5;
            }

			base.OnDamage(newAmount, from, willKill);
		}

        public override void GenerateLoot()
        {
          
            AddLoot(LootPack.Average);     
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));

		}


        public BicheCerynie(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
        public override int Meat => Utility.RandomMinMax(1, 3);
        public override int Hides => Utility.RandomMinMax(3, 5);
		public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override int GetAttackSound()
        {
            return 0x82;
        }

        public override int GetHurtSound()
        {
            return 0x83;
        }

        public override int GetDeathSound()
        {
            return 0x84;
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
