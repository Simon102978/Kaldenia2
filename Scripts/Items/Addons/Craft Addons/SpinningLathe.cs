using Server.Engines.Craft;

namespace Server.Items
{
    public class SpinningLathe : CraftAddon
    {
        public override BaseAddonDeed Deed => new SpinningLatheDeed(Tools.Count > 0 ? Tools[0].UsesRemaining : 0);
        public override CraftSystem CraftSystem => DefCarpentry.CraftSystem;

        [Constructable]
        public SpinningLathe(bool south, int uses)
        {
            if (south)
            {
                AddCraftComponent(new AddonToolComponent(CraftSystem, 39962, 39963, "Machine � bois", uses, this), 0, 0, 0);
                AddComponent(new ToolDropComponent(40006, 1024014), -1, 0, 0);
            }
            else
            {
                AddCraftComponent(new AddonToolComponent(CraftSystem, 39972, 39973, "Machine � bois", uses, this), 0, 0, 0);
                AddComponent(new ToolDropComponent(40007, 1024014), 0, 1, 0);
            }
        }

        public SpinningLathe(Serial serial)
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

    public class SpinningLatheDeed : CraftAddonDeed
    {
        public override int LabelNumber => 1156369;  // spinning lathe
        public override BaseAddon Addon => new SpinningLathe(_South, UsesRemaining);

        private bool _South;

        [Constructable]
        public SpinningLatheDeed() : this(0)
        {
        }

        [Constructable]
        public SpinningLatheDeed(int uses) : base(uses)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.SendGump(new SouthEastGump(s =>
                {
                    _South = s;
                    base.OnDoubleClick(from);
                }));
            }
        }

        public SpinningLatheDeed(Serial serial)
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