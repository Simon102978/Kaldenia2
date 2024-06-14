using Server.Custom.Items.TrainingWeapon;

namespace Server.Items
{
	[Flipable(41560, 41560)]
	public class TrainingDoublelames : BaseTrainingMeleeWeapon
	{
		[Constructable]
		public TrainingDoublelames()
			: base(41560)
		{
			Name = "Double Lames d'entrainement";
		}

		public TrainingDoublelames(Serial serial)
			: base(serial)
		{
		}

		public override int StrengthReq => 1;
		public override int MinDamage => 1;
		public override int MaxDamage => 1;
		public override float Speed => 2.75f;

		public override int DefHitSound => 0x23B;
		public override int DefMissSound => 0x23A;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 65;
		public override SkillName DefSkill => SkillName.Wrestling;
		public override WeaponType DefType => WeaponType.Slashing;
		public override WeaponAnimation DefAnimation => WeaponAnimation.Slash1H;

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
