using System;
using Server.Items;

namespace Server.Items
{
    public class Necklace1 : BaseNecklace
    {
        [Constructable]
        public Necklace1() : base(0x1F08)
        {
            Weight = 0.1;
            Name = "Collier";
        }

        public override int ComputeItemID()
        {
            switch (Resource)
            {
                case CraftResource.Iron:
                case CraftResource.Argent: return 0x1F08;
                case CraftResource.Gold: return 0x1088;
            }

            return base.ComputeItemID();
        }

        public Necklace1(Serial serial) : base(serial)
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

    public class Ring1 : BaseRing
    {
        [Constructable]
        public Ring1() : base(0x1F09)
        {
            Weight = 1.0;
            Name = "Anneau";
        }

        public override int ComputeItemID()
        {
            switch (Resource)
            {
                case CraftResource.Iron:
                case CraftResource.Argent: return 0x1F09;
                case CraftResource.Gold: return 0x108A;
            }

            return base.ComputeItemID();
        }

        public Ring1(Serial serial) : base(serial)
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

    public class Bracelet1 : BaseBracelet
    {
        [Constructable]
        public Bracelet1() : base(0x1F06)
        {
            Weight = 0.1;
            Name = "Bracelet";
        }

        public override int ComputeItemID()
        {
            switch (Resource)
            {
                case CraftResource.Iron:
                case CraftResource.Argent: return 0x1F06;
                case CraftResource.Gold: return 0x1086;
            }

            return base.ComputeItemID();
        }

        public Bracelet1(Serial serial) : base(serial)
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

    public class Earrings1 : BaseEarrings
    {
        [Constructable]
        public Earrings1() : base(0x1F07)
        {
            Weight = 1.0;
            Name = "Boucles d'oreilles";
        }

        public override int ComputeItemID()
        {
            switch (Resource)
            {
                case CraftResource.Iron:
                case CraftResource.Argent: return 0x1F07;
                case CraftResource.Gold: return 0x1087;
            }

            return base.ComputeItemID();
        }

        public Earrings1(Serial serial) : base(serial)
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