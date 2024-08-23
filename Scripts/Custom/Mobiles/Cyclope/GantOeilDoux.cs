using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Items
{
  
    public class GantOeilDoux : BaseRanged
    {
    	public static List<int> RocheID = new List<int>()
		{
			4952,
            4957,
            4962,
            4960,
            4968,
            4963,
            4964,
            4966,
            4965        
		};

        public override int EffectID { get { return RocheID[Utility.Random(RocheID.Count)]; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override int DefHitSound => 0x15E;
        public override int DefMissSound => 0x15E;

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }


        public override SkillName DefSkill => SkillName.Magery;

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
        public GantOeilDoux()
            : base(0x1450)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Name = "Gant d'Oeil Doux";
            LootType = LootType.Newbied;
            Movable = false;
        }


        public override bool OnFired(Mobile attacker, IDamageable damageable)
        {
            


          	if (!attacker.InRange(damageable, 1))
			{
				attacker.MovingEffect(damageable, EffectID, 18, 1, false, false, Hue, 0);
			}

            return base.OnFired(attacker, damageable);
        }









































        public GantOeilDoux(Serial serial)
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
