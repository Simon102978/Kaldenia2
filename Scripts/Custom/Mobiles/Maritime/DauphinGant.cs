using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Items
{
  
    public class DauphinGant : BaseRanged
    {
        public override int EffectID { get { return 0x36FE; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override int DefHitSound => 0x15E;
        public override int DefMissSound => 0x15E;

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }


        public override SkillName DefSkill => SkillName.Archery;

        public override int StrengthReq { get { return 275; } }
		public override int MinDamage => 20;
		public override int MaxDamage => 24;
        public override float Speed { get { return 3.00f; } }

        public override int DefMaxRange { get { return 10; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 60; } }

        public override bool SkipArrow => true;

        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Wrestle; } }

        [Constructable]
        public DauphinGant()
            : base(0x1450)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Name = "Gant des Dauphins";
            LootType = LootType.Newbied;
            Movable = false;
        }


        public override bool OnFired(Mobile attacker, IDamageable damageable)
        {
         
            attacker.MovingParticles(damageable, 0x36D4, 7, 0, false, false, 0xCA, 0, 9502, 4019, 0x160, 0);
        //   attacker.MovingParticles(damageable, 0x36FE, 7, 0, false, true, 0xCA, 10, 9502, 4019, 0x160, 0);

            return base.OnFired(attacker, damageable);
        }

         public override void PlaySwingAnimation(Mobile from)
        {
            // Dauphin a pas d'animation d'Attaque, sans sa il disparait.
            
        }

        public DauphinGant(Serial serial)
            : base(serial)
        {
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
