namespace Server.Items
{
	public abstract class BaseCrossbow : BaseRanged
	{
		public BaseCrossbow(int itemID)
			: base(itemID)
		{
		}

		public BaseCrossbow(Serial serial)
			: base(serial)
		{
		}

		public override SkillName DefSkill => SkillName.Archery;

		public override WeaponType DefType => WeaponType.Ranged;

		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootXBow;
		public override int DefMaxRange => 6;

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

		public override void OnDoubleClick(Mobile from)
		{
		}
	}
}