using Server.Items;

namespace Server.Custom.Items.TrainingWeapon
{
	public abstract class BaseTrainingMeleeWeapon : BaseMeleeWeapon
	{
		public BaseTrainingMeleeWeapon(int itemID)
			: base(itemID)
		{
			Layer = Layer.OneHanded;
			Weight = 1.0;
		}

		public BaseTrainingMeleeWeapon(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
