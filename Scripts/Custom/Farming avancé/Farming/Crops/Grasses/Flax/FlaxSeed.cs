namespace Server.Items.Crops
{
    public class FlaxSeed : BaseSeed
    {
        public override bool CanGrowGarden { get { return true; } }

        [Constructable]
        public FlaxSeed() : this(1) { }

        [Constructable]
        public FlaxSeed(int amount)
            : base(0xF27)
        {
            Stackable = true;
            Weight = .1;
            Hue = 0x5E2;
            Movable = true;
            Amount = amount;
            Name = "Flax Seed";
        }

        public override void OnDoubleClick(Mobile from)
        {
			Sow(from, typeof(FlaxSeedling));
        }

        public FlaxSeed(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}