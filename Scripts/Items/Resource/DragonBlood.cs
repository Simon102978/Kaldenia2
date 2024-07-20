namespace Server.Items
{
    public class DragonBlood : BaseReagent, ICommodity
    {
        [Constructable]
        public DragonBlood()
            : this(1)
        {
        }

        [Constructable]
        public DragonBlood(int amount)
            : base(0x4077, amount)
        {
			Name = "Sang de Dragon";
			Amount = amount;
			Stackable = true;
			if (Amount <= 0)
				Amount = 1;
		}

        public DragonBlood(Serial serial)
            : base(serial)
        {
        }
		public override double DefaultWeight => 1.0;

		public override void OnAfterDuped(Item newItem)
		{
			base.OnAfterDuped(newItem);

			if (newItem is DragonBlood blood && blood.Amount <= 0)
				blood.Amount = 1;
		}

		TextDefinition ICommodity.Description => LabelNumber;
       bool ICommodity.IsDeedable => true;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
			writer.Write(Amount);
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			Amount = reader.ReadInt();
			if (Amount <= 0)
				Amount = 1;
		}
    }
}
