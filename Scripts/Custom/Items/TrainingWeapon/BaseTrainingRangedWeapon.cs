using Server.Items;

namespace Server.Custom.Items.TrainingWeapon
{
	public abstract class BaseTrainingRangedWeapon : BaseRanged
	{
		public BaseTrainingRangedWeapon(int itemID)
			: base(itemID)
		{
			Layer = Layer.TwoHanded;
			Weight = 1.0;
		}

		public BaseTrainingRangedWeapon(Serial serial)
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
