using System;
using System.Collections.Generic;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
    [CorpseName("un corps de serpent de mer")]
    [TypeAlias("Server.Mobiles.Seaserpant")]
    public class SeaSerpent : BaseCreature
    {
      	private DateTime m_NextStuck;
        public DateTime DelayOnHit1;
        
        [Constructable]
        public SeaSerpent()
            : base(AIType.MaritimeMageAI, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un serpent de mer";
            Body = 150;
            BaseSoundID = 447;

            Hue = Utility.Random(0x530, 9);

            SetStr(500);
            SetDex(100);
            SetInt(1000);

            SetHits(600);
            SetMana(750);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 15, 20);

            SetSkill(SkillName.Hiding, 80.0);
            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 150.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 100.1, 100.0);
            SetSkill(SkillName.Meditation, 100, 120.0);

            Fame = 6000;
            Karma = -6000;

            CanSwim = true;
            CantWalk = true;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public void StuckBoat()
		{
			if (m_NextStuck < DateTime.UtcNow)
			{

				IPooledEnumerable eable = this.GetItemsInRange(10);

				foreach (Item m in eable)
				{
					if (m is BaseBoat boat)
					{
						if (!boat.Stuck)
						{
							Emote("*Coince le navire avec sa queue.*");
							boat.Stuck = true;							
						}						
					}
				}

				m_NextStuck = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(45, 60));
			
				eable.Free();		

			}

		}

       	public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

            if (Utility.RandomDouble() < 0.25) 
                StuckBoat();
            else
            {
                if (DelayOnHit1 < DateTime.UtcNow)
                {
                    Cleave();
                }				
            }

          

			base.AlterMeleeDamageTo(to, ref damage);
		}

        	public void Cleave()
		{

			if (DelayOnHit1 < DateTime.UtcNow)
			{
				DelayOnHit1 = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 20)); // mis la, parce que sinon boucle infinie.

				int Range = 3;
				List<Mobile> targets = new List<Mobile>();

				IPooledEnumerable eable = this.GetMobilesInRange(Range);

				foreach (Mobile m in eable)
				{
					if (this != m  && !m.IsStaff())
					{
						if (m is BaseCreature bc && bc.Tribe == TribeType.Titusien)
						{
							continue;
						}



						targets.Add(m);
					}
				}

				eable.Free();

				Emote("*Frappes violament*");

				if (targets.Count > 0)
				{

					int dmg = 25;



					for (int i = 0; i < targets.Count; ++i)
					{
						Mobile m = targets[i];


						DoHarmful(m);
						AOS.Damage(m, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

						m.Freeze(TimeSpan.FromSeconds(3));

					
					}
				}


			

			}





		}

        public SeaSerpent(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(5, 10);

		public override MeatType MeatType => MeatType.SeaSerpentSteak;

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Demoniaque;

		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Demoniaque;

        public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 3;


		public override void GenerateLoot()
        {
         	AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.LootItem<RawFishSteak>());
            AddLoot(LootPack.RandomLootItem(new[] { typeof(SulfurousAsh), typeof(BlackPearl) }, 100.0, 4, false, true));
			

		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			if (version == 0)
			{
				AI = AIType.MaritimeMageAI;
			}
		}
    }
}
