using System;

namespace Server.Items.Crops
{
	public class PinkCarnationSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PinkCarnationSeed() : this( 1 ) { }

		[Constructable]
		public PinkCarnationSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Pink Carnation Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PinkCarnationSeedling));
		}

		public PinkCarnationSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PinkCarnationSeedling : BaseSeedling
	{
		[Constructable]
		public PinkCarnationSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Pink Carnation Seedling";
			Sower = sower;
			Init(this, typeof(PinkCarnationCrop));
		}
		
		public PinkCarnationSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PinkCarnationCrop));
		}
	}

	public class PinkCarnationCrop : BaseCrop
	{
		[Constructable]
		public PinkCarnationCrop() : this(null) { }

		[Constructable]
		public PinkCarnationCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Pink Carnation Plant";
			Hue = 2999;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(PinkCarnation));
		}

		public PinkCarnationCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0x234C, 0x234D, true);
		}
	}
}