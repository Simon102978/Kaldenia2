using System;
using Server;
using Server.Items;
using Server.Targets;
using Server.Mobiles;

namespace Server.Items
{

	public class BazFaux : BaseSword // Katana
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.Disarm;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 25;
		public override int MinDamage => 10;
		public override int MaxDamage => 14;
		public override float Speed => 2.50f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 90;
		[Constructable]
		public BazFaux()
			: base(0xA49B) 
		{
		
			Name = "Faux";
			Weight = 8.0;
			Layer = Layer.TwoHanded;
		}

		public BazFaux(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class BazFourche : BaseSword // Katana
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.Disarm;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 25;
		public override int MinDamage => 10;
		public override int MaxDamage => 14;
		public override float Speed => 2.50f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 90;
		[Constructable]
		public BazFourche()
			: base(0xA4B2)
		{

			Name = "Fourche";
			Weight = 8.0;
			Layer = Layer.TwoHanded;
		}

		public BazFourche(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	public class BazPelle : BaseSword // Katana
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.Disarm;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 25;
		public override int MinDamage => 10;
		public override int MaxDamage => 14;
		public override float Speed => 2.50f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 90;
		[Constructable]
		public BazPelle()
			: base(0xA4B4)
		{

			Name = "Pelle";
			Weight = 8.0;
			Layer = Layer.TwoHanded;
		}

		public BazPelle(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	public class BazPioche : BaseSword // Katana
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.Disarm;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 25;
		public override int MinDamage => 10;
		public override int MaxDamage => 14;
		public override float Speed => 2.50f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 90;
		[Constructable]
		public BazPioche()
			: base(0xA4B5)
		{

			Name = "Pioche";
			Weight = 8.0;
		//	Layer = Layer.TwoHanded;
		}

		public BazPioche(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
