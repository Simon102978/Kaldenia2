namespace Server.Items.Crops
{
    public class SugarcaneSeed : BaseSeed
    {
        public override bool CanGrowSand { get { return true; } }

       	public override double MinSkill{ get { return 0.0; } }

		public override double MaxSkill{ get { return 40.0; } }


        [Constructable]
        public SugarcaneSeed() : this(1) { }

        [Constructable]
        public SugarcaneSeed(int amount)
            : base(0xF27)
        {
            Stackable = true;
            Weight = .1;
            Hue = 0x5E2;
            Movable = true;
            Amount = amount;
            Name = "Sugar Cane Seed";
        }

        public override void OnDoubleClick(Mobile from)
        {
			Sow(from, typeof(SugarcaneSeedling));
        }

        public SugarcaneSeed(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}