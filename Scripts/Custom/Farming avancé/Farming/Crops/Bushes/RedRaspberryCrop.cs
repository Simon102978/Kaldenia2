namespace Server.Items.Crops
{
	public class RedRaspberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public RedRaspberrySeed() : this( 1 ) { }

		[Constructable]
		public RedRaspberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Red Raspberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(RedRaspberrySeedling));
		}

		public RedRaspberrySeed( Serial serial ) : base( serial ) { }

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

	public class RedRaspberrySeedling : BaseSeedling
	{
		[Constructable]
		public RedRaspberrySeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Red Raspberry Seedling";
			Sower = sower;
            Init(this, typeof(RedRaspberryCrop));
		}
		
		public RedRaspberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

            Init(this, typeof(RedRaspberryCrop));
		}
	}

	public class RedRaspberryCrop : BaseCrop
	{
		[Constructable]
		public RedRaspberryCrop() : this(null) { }

		[Constructable]
		public RedRaspberryCrop( Mobile sower ) : base( 3272 )
		{
			Movable = false;
			Name = "Red Raspberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 15, 3272, 3272, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(RedRaspberry));
		}

		public RedRaspberryCrop(Serial serial) : base(serial) { }

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