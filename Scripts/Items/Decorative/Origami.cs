namespace Server.Items
{
    public class OrigamiPaper : Item
    {
        [Constructable]
        public OrigamiPaper()
            : base(0x2830)
        {
        }

        public OrigamiPaper(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030288;// origami paper

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                Delete();

                Item i = null;

                switch (Utility.Random((from.BAC >= 7) ? 8 : 7)) //switch ( Utility.Random( (from.BAC >= 5) ? 6 : 5) )
                {
                    case 0: i = new OrigamiButterfly(); break;
                    case 1: i = new OrigamiSwan(); break;
                    case 2: i = new OrigamiFrog(); break;
                    case 3: i = new OrigamiShape(); break;
                    case 4: i = new OrigamiSongbird(); break;
                    case 5: i = new OrigamiFish(); break;
                    case 6: i = new OrigamiDragon(); break;
                    case 7: i = new OrigamiBunny(); break;
                }

                if (i != null)
                    from.AddToBackpack(i);

                from.SendLocalizedMessage(1070822); // You fold the paper into an interesting shape.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiButterfly : Item, IDyable
    {
        [Constructable]
        public OrigamiButterfly()
            : base(0x2838)
        {
			Name = "Papillon de cire";
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiButterfly(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030296;// a delicate origami butterfly
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiSwan : Item, IDyable
    {
        [Constructable]
        public OrigamiSwan()
            : base(0x2839)
        {
			Name = "Un Cygne de Cire";
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiSwan(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030297;// a delicate origami swan
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiFrog : Item, IDyable
    {
        [Constructable]
        public OrigamiFrog()
            : base(0x283A)
        {
			Name = "Une Grenouille de Cire";
        }
		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}
		public OrigamiFrog(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030298;// a delicate origami frog
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiShape : Item, IDyable
    {
        [Constructable]
        public OrigamiShape()
            : base(0x283B)
        {
			Name = "Une forme de Cire";
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiShape(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030299;// an intricate geometric origami shape
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiSongbird : Item, IDyable
    {
        [Constructable]
        public OrigamiSongbird()
            : base(0x283C)
        {
			Name = "Un Oiseau de Cire";
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiSongbird(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030300;// a delicate origami songbird
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiFish : Item, IDyable
    {
        [Constructable]
        public OrigamiFish()
            : base(0x283D)
        {
			Name = "Un poisson de Cire";
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiFish(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1030301;// a delicate origami fish
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class OrigamiDragon : Item, IDyable
    {
        //public override int LabelNumber{ get{ return 1030296; } } // a delicate origami butterfly

        [Constructable]
        public OrigamiDragon()
            : base(0x4B1C)
        {

			Name = "Un Dragon de Cire";
            Weight = 1.0;
          
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiDragon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    // [FlipableAttribute( 0x4B1E, 0x4B1F )]
    public class OrigamiBunny : Item, IDyable
    {
        //public override int LabelNumber{ get{ return 1030296; } } // a delicate origami butterfly

        [Constructable]
        public OrigamiBunny()
            : base(0x4B1F)
        {
			Name = "Un Lapin de Cire";
            Weight = 1.0;
            
        }

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public OrigamiBunny(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}