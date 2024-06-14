using System;
using Server.Custom.Items.TrainingWeapon;

namespace Server.Items
{
	[Flipable(0x13B2, 0x13B1)]
	public class TrainingBow : BaseTrainingRangedWeapon
	{
		[Constructable]
		public TrainingBow() : base(0x13B2)
		{
			Name = "Arc d'entrainement";
			Layer = Layer.TwoHanded;
		}

		public TrainingBow(Serial serial)
			: base(serial)
		{
		}

		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override int StrengthReq => 30;
		public override int MinDamage => 1;
		public override int MaxDamage => 1;
		public override float Speed => 4.25f;

		public override int DefMaxRange => 7;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 100;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

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
