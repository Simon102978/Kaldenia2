namespace Server.Items
{
    public class FarmableFougere : FarmableCrop
    {
        public override double SkillNecessaire => 32.1;

    [Constructable]
    public FarmableFougere()
        : base(GetCropID())
    {
        Name ="Fougère";
    }

    public FarmableFougere(Serial serial)
        : base(serial)
    {
    }

    public static int GetCropID()
    {
        return 15690;
    }

    public override Item GetCropObject()
    {
        Fougere onion = new Fougere();
        return onion;
    }


    public override void Serialize(GenericWriter writer)
    {
        base.Serialize(writer);

        writer.WriteEncodedInt(0); // version
    }

    public override void Deserialize(GenericReader reader)
    {
        base.Deserialize(reader);

        int version = reader.ReadEncodedInt();
    }
}

    public class FarmableDentDeLion : FarmableCrop
    {
         public override double SkillNecessaire => 51.5;

        [Constructable]
        public FarmableDentDeLion()
                    : base(GetCropID())
                {
            Name ="Dent-de-lion";
        }

        public FarmableDentDeLion(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15814;
        }

        public override Item GetCropObject()
        {
            DentDeLion onion = new DentDeLion();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableLilas : FarmableCrop
    {
        public override double SkillNecessaire => 77.1;

        [Constructable]
        public FarmableLilas()
                    : base(GetCropID())
                {
            Name ="Lilas";
        }

        public FarmableLilas(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15746;
        }

        public override Item GetCropObject()
        {
            Lilas onion = new Lilas();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableTulipe : FarmableCrop
    {
        public override double SkillNecessaire => 69.2;

        [Constructable]
        public FarmableTulipe()
                    : base(GetCropID())
                {
            Name ="Tulipe";
        }

        public FarmableTulipe(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15837;
        }

        public override Item GetCropObject()
        {
            Tulipe onion = new Tulipe();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableRoseSauvage : FarmableCrop
    {
        public override double SkillNecessaire => 92.0;

        [Constructable]
        public FarmableRoseSauvage()
                    : base(GetCropID())
                {
            Name ="Rose sauvage";
        }

        public FarmableRoseSauvage(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15816;
        }

        public override Item GetCropObject()
        {
            RoseSauvage onion = new RoseSauvage();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableIndigo : FarmableCrop
    {
        public override double SkillNecessaire => 65.4;

        [Constructable]
        public FarmableIndigo()
                    : base(GetCropID())
                {
            Name ="Indigo";
        }

        public FarmableIndigo(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15836;
        }

        public override Item GetCropObject()
        {
            Indigo onion = new Indigo();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableLys : FarmableCrop
    {
        public override double SkillNecessaire => 82.4;

        [Constructable]
        public FarmableLys()
                    : base(GetCropID())
                {
            Name ="Lys";
        }

        public FarmableLys(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15821;
        }

        public override Item GetCropObject()
        {
            Lys onion = new Lys();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableSafran : FarmableCrop
    {
        public override double SkillNecessaire => 85.5;

        [Constructable]
        public FarmableSafran()
                    : base(GetCropID())
                {
            Name ="Safran";
        }

        public FarmableSafran(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15738;
        }

        public override Item GetCropObject()
        {
            Safran onion = new Safran();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableOmbrella : FarmableCrop
    {
        public override double SkillNecessaire => 97.9;

        [Constructable]
        public FarmableOmbrella()
                    : base(GetCropID())
                {
            Name ="Ombrella";
        }

        public FarmableOmbrella(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15822;
        }

        public override Item GetCropObject()
        {
            Ombrella onion = new Ombrella();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableRoseTremiere : FarmableCrop
    {
        public override double SkillNecessaire => 73.0;

        [Constructable]
        public FarmableRoseTremiere()
                    : base(GetCropID())
                {
            Name ="Rose trémière";
        }

        public FarmableRoseTremiere(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15835;
        }

        public override Item GetCropObject()
        {
            RoseTremiere onion = new RoseTremiere();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableCamomille : FarmableCrop
    {
        public override double SkillNecessaire => 64.2;

        [Constructable]
        public FarmableCamomille()
                    : base(GetCropID())
                {
            Name ="Camomille";
        }

        public FarmableCamomille(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15815;
        }

        public override Item GetCropObject()
        {
            Camomille onion = new Camomille();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableDahlia : FarmableCrop
    {
        public override double SkillNecessaire => 79.4;

        [Constructable]
        public FarmableDahlia()
                    : base(GetCropID())
                {
            Name ="Dahlia";
        }

        public FarmableDahlia(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15830;
        }

        public override Item GetCropObject()
        {
            Dahlia onion = new Dahlia();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
	public class FarmableHellebore : FarmableCrop
	{
		public override double SkillNecessaire => 00.0;

		[Constructable]
		public FarmableHellebore()
					: base(GetCropID())
		{
			Name = "Hellebore";
		}

		public FarmableHellebore(Serial serial)
					: base(serial)
		{
		}

		public static int GetCropID()
		{
			return 3132;
		}

		public override Item GetCropObject()
		{
			Hellebore onion = new Hellebore();
			return onion;
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadEncodedInt();
		}
	}
	public class FarmableGeuleDeDragon : FarmableCrop
    {
        public override double SkillNecessaire => 85.2;

        [Constructable]
        public FarmableGeuleDeDragon()
                    : base(GetCropID())
                {
            Name ="Gueule de dragon";
        }

        public FarmableGeuleDeDragon(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15744;
        }

        public override Item GetCropObject()
        {
            GeuleDeDragon onion = new GeuleDeDragon();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableCramelia : FarmableCrop
    {
        public override double SkillNecessaire => 88.7;

        [Constructable]
        public FarmableCramelia()
                    : base(GetCropID())
                {
            Name ="Cramélia";
        }

        public FarmableCramelia(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15749;
        }

        public override Item GetCropObject()
        {
            Cramelia onion = new Cramelia();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class FarmableGivrelle : FarmableCrop
    {
        public override double SkillNecessaire => 65.7;

        [Constructable]
        public FarmableGivrelle()
                    : base(GetCropID())
                {
            Name ="Givrelle";
        }

        public FarmableGivrelle(Serial serial)
                    : base(serial)
                {
        }

        public static int GetCropID()
        {
            return 15831;
        }

        public override Item GetCropObject()
        {
            Givrelle onion = new Givrelle();
            return onion;
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
