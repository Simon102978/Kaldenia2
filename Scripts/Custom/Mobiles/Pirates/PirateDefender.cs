using Server.Items;
using Server.Misc;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Pirate")]
	public class PirateDefender : PirateBase
	{
        public bool BlockReflect { get; set; }

        [Constructable]
        public PirateDefender()
            : this(0)
        {

           
        }

		[Constructable]
        public PirateDefender(int PirateBoatId)
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4, PirateBoatId)
        {
           
            		
			SetStr(200, 250);
			SetDex(61, 85);
			SetInt(75, 100);
            SetHits(300,400);

            SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);


			SetSkill(SkillName.ArmsLore, 80.0, 100.0);
            SetSkill(SkillName.Parry, 100.0, 120.0);
            SetSkill(SkillName.Swords, 100.0, 120.0);


            AddItem(new Targe());
            AddItem(new JavelotLuxe());
             
        }

           
        



		public override void OnThink()
		{
			base.OnThink();

		

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

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

		 	Hits += AOS.Scale(damage, 50);

			Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x377A, to.Location, to.Location, 1, 15, false, false, 1926, 0, 0, 9502, 1, to.Serial, 16, 0));
            Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x3728, to.Location, to.Location, 1, 12, false, false, 1963, 0, 0, 9042, 1, to.Serial, 16, 0));

			Parole();

			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{

				base.OnDamage(amount, from, willKill);

		}

	
		public override void ThrowingDetonate (Mobile m)
		{
 				
                if (m != null)
                {
                      DoHarmful(m);
                      m.Paralyze(TimeSpan.FromSeconds(3));
                    
                }
		}	

		public PirateDefender(Serial serial)
            : base(serial)
        {
        }

		public override bool ClickTitle => false;


		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
