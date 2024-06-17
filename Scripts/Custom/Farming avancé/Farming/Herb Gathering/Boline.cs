using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute(0x26BB, 0x26BB)]
	public class Boline : BaseBoline
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }

		

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public Boline() : this( 0x26BB, 50 )
		{
            this.Name = "Serpe";
			Hue = 1940;
		}

		[Constructable]
		public Boline(int usesremaining) : this(0x26BB, usesremaining )
		{
            this.Name = "Serpe";
			Hue = 1940;
		}

		[Constructable]
		public Boline(int itemid, int usesremaining) : base( itemid, usesremaining )
		{
            this.Name = "Serpe";
			Hue = 1940;
		}

		public Boline( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}