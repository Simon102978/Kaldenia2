namespace Server.Items
{
    public class Vase : Item
    {
        [Constructable]
        public Vase()
            : base(0xB46)
        {
            Weight = 1.0;
        }

        public Vase(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LargeVase : Item
    {
        [Constructable]
        public LargeVase()
            : base(0xB45)
        {
            Weight = 1.0;
        }

        public LargeVase(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class SmallUrn : Item
    {
        [Constructable]
        public SmallUrn()
            : base(0x241C)
        {
            Weight = 1.0;
        }

        public SmallUrn(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	public class MiniCherryTree1 : Item
	{
		[Constructable]
		public MiniCherryTree1()
			: base(0x9952)
		{
			Weight = 1.0;
			Name = "Arbre en pot";
		}

		public MiniCherryTree1(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	public class Orchid1 : Item
	{
		[Constructable]
		public Orchid1()
			: base(0x9953)
		{
			Weight = 1.0;
			Name = "Orchid�e";
		}

		public Orchid1(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	public class Orchid2 : Item
	{
		[Constructable]
		public Orchid2()
			: base(0x9954)
		{
			Weight = 1.0;
			Name = "Orchid�e";
		}

		public Orchid2(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}