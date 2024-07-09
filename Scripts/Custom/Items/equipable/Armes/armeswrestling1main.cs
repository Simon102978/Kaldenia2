using System;
using Server;
using Server.Items;
using Server.Targets;
using Server.Mobiles;

namespace Server.Items
{
	public class Nunchakuonehand : BaseKatar
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.Feint;
		public override WeaponAbility SecondaryAbility => WeaponAbility.DoubleStrike;
		public override int StrengthReq => 40;
		public override int MinDamage => 9;
		public override int MaxDamage => 12;
		public override float Speed => 2.75f;
		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 65;

		[Constructable]
		public Nunchakuonehand() : base(0x27AE)
		{

			Name = "Nunchaku une main";
			Weight = 8.0;
			Layer = Layer.OneHanded;
		}

		public Nunchakuonehand(Serial serial)
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

	public class LameAffutee : BaseKatar
	{
		public override WeaponAbility PrimaryAbility => WeaponAbility.PsychicAttack;
		public override WeaponAbility SecondaryAbility => WeaponAbility.BleedAttack;
		public override int StrengthReq => 35;
		public override int MinDamage => 8;
		public override int MaxDamage => 11;
		public override float Speed => 2.50f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 65;

		[Constructable]
		public LameAffutee() : base(0xA24D)
		{
			Name = "Lame Affutée";
			Weight = 8.0;
			Layer = Layer.OneHanded;
		}

		public LameAffutee(Serial serial)
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

