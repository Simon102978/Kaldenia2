using System;
using Server.Items;

namespace Server.Items
{
    public abstract class BaseFlower : Item
    {
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 0:
                    {
                        break;
                    }
            }
        }


        public BaseFlower(int itemId)
            : base(itemId)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public BaseFlower(Serial serial)
            : base(serial)
        {
        }
    }

    public class Fougere : BaseFlower
    {

        [Constructable]
        public Fougere()
                : this(1)
        {
        }

        [Constructable]
        public Fougere(int amount)
          : base(15690)
        {
            Name ="Fougère";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Fougere(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DentDeLion : BaseFlower
    {
            [Constructable]
            public DentDeLion()
                    : this(1)
                {
            }

            [Constructable]
            public DentDeLion(int amount)
                  : base(15814)
                {
                Name ="Dent-de-lion";
                Stackable = true;
                Weight = 0.5;
                Amount = amount;
            }

            public DentDeLion(Serial serial)
                    : base(serial)
                {
            }

                public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Lilas : BaseFlower
    {
        [Constructable]
        public Lilas()
                : this(1)
        {
        }

        [Constructable]
        public Lilas(int amount)
              : base(15746)
        {
            Name ="Lilas";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Lilas(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
        base.Serialize(writer);

        writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
        base.Deserialize(reader);

        int version = reader.ReadInt();
        }
    }

    public class Tulipe : BaseFlower
    {
            [Constructable]
            public Tulipe()
                    : this(1)
                {
            }

            [Constructable]
            public Tulipe(int amount)
                  : base(15837)
                {
                Name ="Tulipe";
                Stackable = true;
                Weight = 0.5;
                Amount = amount;
            }

            public Tulipe(Serial serial)
                    : base(serial)
                {
            }

                public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RoseSauvage : BaseFlower
    {

        [Constructable]
        public RoseSauvage()
                : this(1)
        {
        }

        [Constructable]
        public RoseSauvage(int amount)
              : base(15816)
        {
            Name ="Rose sauvage";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public RoseSauvage(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
        base.Serialize(writer);

        writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Indigo : BaseFlower
    {
            [Constructable]
            public Indigo()
                    : this(1)
            {
            }

            [Constructable]
            public Indigo(int amount)
                  : base(15836)
            {
                Name ="Indigo";
                Stackable = true;
                Weight = 0.5;
                Amount = amount;
            }

            public Indigo(Serial serial)
                    : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }
    }

    public class Lys : BaseFlower
    {
        [Constructable]
        public Lys()
                : this(1)
        {
        }

        [Constructable]
        public Lys(int amount)
              : base(15821)
        {
            Name ="Lys";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Lys(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Safran : BaseFlower
    {
            [Constructable]
            public Safran()
                    : this(1)
                {
            }

            [Constructable]
            public Safran(int amount)
                  : base(15738)
                {
                Name ="Safran";
                Stackable = true;
                Weight = 0.5;
                Amount = amount;
            }

            public Safran(Serial serial)
                    : base(serial)
                {
            }

                public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Ombrella : BaseFlower
    {
        [Constructable]
        public Ombrella()
                : this(1)
        {
        }

        [Constructable]
        public Ombrella(int amount)
              : base(15822)
        {
            Name ="Ombrella";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Ombrella(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RoseTremiere : BaseFlower
    {
        [Constructable]
        public RoseTremiere()
                : this(1)
        {
        }

        [Constructable]
        public RoseTremiere(int amount)
              : base(15835)
            {
            Name ="Roses trémières";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public RoseTremiere(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Camomille : BaseFlower
    {
        [Constructable]
        public Camomille()
            : this(1)
        {
        }

        [Constructable]
        public Camomille(int amount)
           : base(15815)
        {
           Name ="Camomille";
           Stackable = true;
           Weight = 0.5;
           Amount = amount;
        }

        public Camomille(Serial serial)
          : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Dahlia : BaseFlower
    {
        [Constructable]
        public Dahlia()
                : this(1)
        {
        }

        [Constructable]
        public Dahlia(int amount)
              : base(15830)
            {
            Name ="Dahlia";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Dahlia(Serial serial)
                : base(serial)
            {
        }

                public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class GeuleDeDragon : BaseFlower
    {
    [Constructable]
    public GeuleDeDragon()
            : this(1)
        {
    }

    [Constructable]
    public GeuleDeDragon(int amount)
          : base(15744)
    {
        Name = "Geule de dragon";
        Stackable = true;
        Weight = 0.5;
        Amount = amount;
    }

        public GeuleDeDragon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
}
    }

    public class Cramelia : BaseFlower
    {
        [Constructable]
        public Cramelia()
                : this(1)
        {
        }

        [Constructable]
        public Cramelia(int amount)
              : base(15749)
            {
            Name ="Cramélia";
            Stackable = true;
            Weight = 0.5;
            Amount = amount;
        }

        public Cramelia(Serial serial)
                : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
        base.Serialize(writer);

        writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Givrelle : BaseFlower
    {
            [Constructable]
            public Givrelle()
                    : this(1)
                {
            }

            [Constructable]
            public Givrelle(int amount)
                  : base(15831)
                {
                Name ="Givrelle";
                Stackable = true;
                Weight = 0.5;
                Amount = amount;
            }

            public Givrelle(Serial serial)
                    : base(serial)
                {
            }

                public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

}
