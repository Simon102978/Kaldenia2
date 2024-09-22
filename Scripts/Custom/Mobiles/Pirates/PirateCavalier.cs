using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("Corps de Pirate")]
    public class PirateCavalier : PirateBase
	{

		[Constructable]
        public PirateCavalier()
            : this(0)
        {

           
        }


        [Constructable]
        public PirateCavalier(int PirateBoatId)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4, PirateBoatId)
        {

			
			double dActiveSpeed = 0.05;
			double dPassiveSpeed = 0.1;


			SpeedInfo.GetCustomSpeeds(this, ref dActiveSpeed, ref dPassiveSpeed);

			ActiveSpeed = dActiveSpeed;
			PassiveSpeed = dPassiveSpeed;
			CurrentSpeed = dPassiveSpeed;


			SetStr(251, 270);
			SetDex(92, 130);
			SetInt(51, 65);

			SetDamage(20, 30);

			SetHits(251, 350);
			SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);


            SetSkill(SkillName.Fencing, 100, 120);
            SetSkill(SkillName.Healing, 60.3, 90.0);
            SetSkill(SkillName.MagicResist, 82.5, 105.0);
            SetSkill(SkillName.Swords, 70, 110);
            SetSkill(SkillName.Tactics, 90.5, 120.0);

			
			AddItem(new Trident());
				


		

			new Horse().Rider = this;

			AdjustSpeeds();

		}


		public override void AdjustSpeeds()
		{
			double activeSpeed = 0.0;
			double passiveSpeed = 0.0;


			if (Mounted)
			{
				SpeedInfo.GetCustomSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}
			else
			{
				SpeedInfo.GetSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}

			

			ActiveSpeed = activeSpeed;
			PassiveSpeed = passiveSpeed;
			CurrentSpeed = passiveSpeed;
		}


		public PirateCavalier(Serial serial)
            : base(serial)
        {
        }


		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			AdjustSpeeds();

			base.OnDamage(amount, from, willKill);
		}
		public override int Meat => 1;
        public override bool AlwaysMurderer => true;
        public override bool ShowFameTitle => false;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;



        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 4));
			base.GenerateLoot();
		}

        public override bool OnBeforeDeath()
        {
            IMount mount = Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Delete();

            return base.OnBeforeDeath();
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
