using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un dauphin")]
    public class DauphinMalefiqueRouge : BaseCreature
    {
        public override bool CanStealth => true;  //Stays Hidden until Combatant in range.

        [Constructable]
        public DauphinMalefiqueRouge()
            : base(AIType.MaritimeArcherAI, FightMode.Aggressor, 10, 8, 0.2, 0.4)
        {
            Name = "Dauphin malefique Rouge";
            Body = 0x97;
            BaseSoundID = 0x8A;
            Hue = 1157;

        	SetStr(208, 319);
            SetInt(45, 91);

            SetHits(200, 300);

            SetDamage(10, 15);              
  
            SetDamageType(ResistanceType.Cold, 100);
         
            SetResistance(ResistanceType.Physical, 55, 62);
            SetResistance(ResistanceType.Fire, 40, 48);
            SetResistance(ResistanceType.Cold, 71, 80);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.Hiding, 90.3, 100.5);
            SetSkill(SkillName.Archery, 90.3, 100.5);
            SetSkill(SkillName.Wrestling, 75.3, 90.5);
            SetSkill(SkillName.Tactics, 75.5, 90.8);
            SetSkill(SkillName.MagicResist, 102.8, 117.9);
            SetSkill(SkillName.Anatomy, 75.5, 90.2);

            Fame = 500;
            Karma = 2000;

            CanSwim = true;
            CantWalk = true;

            AddItem(new DauphinGant());

        }

        public DauphinMalefiqueRouge(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;
       	public override bool CanReveal => false;
        public override int Meat => 1;


        public override int TreasureMapLevel => Utility.RandomList(1, 2);
        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                Jump();
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,2);

		}

       	public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

		 	Hits += AOS.Scale(damage, 50);

			Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x377A, to.Location, to.Location, 1, 15, false, false, 1926, 0, 0, 9502, 1, to.Serial, 16, 0));
            Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x3728, to.Location, to.Location, 1, 12, false, false, 1963, 0, 0, 9042, 1, to.Serial, 16, 0));

			base.AlterMeleeDamageTo(to, ref damage);
		}

        public virtual void Jump()
        {
            if (Utility.RandomBool())
                Animate(3, 16, 1, true, false, 0);
            else
                Animate(4, 20, 1, true, false, 0);
        }

               public override void OnDamage(int amount, Mobile from, bool willKill)
		{
            RevealingAction();
			base.OnDamage(amount, from, willKill);
		}

        public override void OnDamagedBySpell(Mobile from)
        {
            RevealingAction();
            base.OnDamagedBySpell(from);
        }

           public override void OnThink()
        {

            if (!Alive || Deleted)
            {
                return;
            }

            if (!Hidden)
            {
                double chance = 0.05;

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
