namespace Server.Items.Crops
{
	public class BlackRaspberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BlackRaspberrySeed() : this( 1 ) { }

		[Constructable]
		public BlackRaspberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = 0.1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Black Raspberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BlackRaspberrySeedling));
		}

		public BlackRaspberrySeed( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class BlackRaspberrySeedling : BaseSeedling
	{
		[Constructable]
		public BlackRaspberrySeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Black Raspberry Seedling";
			Sower = sower;
            Init(this, typeof(BlackRaspberryCrop));
		}
		public BlackRaspberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BlackRaspberryCrop));
		}
	}

	public class BlackRaspberryCrop : BaseCrop
	{
		[Constructable]
		public BlackRaspberryCrop() : this(null) { }

		[Constructable]
		public BlackRaspberryCrop( Mobile sower ) : base( 3272 )
		{
			Movable = false;
			Name = "Black Raspberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 15, 3272, 3272, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(BlackRaspberry));
		}

		public BlackRaspberryCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 15, 3272, 3272, true);
		}
	}
}