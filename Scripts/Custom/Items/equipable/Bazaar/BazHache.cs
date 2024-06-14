using System;
using Server;
using Server.Items;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Items
{

	public class BazHacheDaedric : BaseAxe // battleAxe
	{
		[Constructable]
		public BazHacheDaedric()
			: base(0xA490)
		{
			Weight = 4.0;
			Name = "Hache Daedric";
		}

		public BazHacheDaedric(Serial serial)
			: base(serial)
		{
		}
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.DoubleStrike;
		public override int StrengthReq => 35;
		public override int MinDamage => 16;
		public override int MaxDamage => 19;
		public override float Speed => 3.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;

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

	public class BazHacheElfique : BaseAxe //axe
	{
		[Constructable]
		public BazHacheElfique()
			: base(0xA49F)
		{
			Weight = 4.0;
			Name = "Hache Elfique";
		}

		public BazHacheElfique(Serial serial)
			: base(serial)
		{
		}

		public override WeaponAbility PrimaryAbility => WeaponAbility.Dismount;
		public override WeaponAbility SecondaryAbility => WeaponAbility.ForceOfNature;

		public override int StrengthReq => 35;
		public override int MinDamage => 16;
		public override int MaxDamage => 19;
		public override float Speed => 3.00f;

		public override int DefHitSound => 0x233;
		public override int DefMissSound => 0x239;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;

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
