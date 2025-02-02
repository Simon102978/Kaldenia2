using Server.Engines.Craft;

namespace Server.Items
{
    public class MalletAndChisel : BaseTool
    {
        [Constructable]
        public MalletAndChisel()
            : base(0x0FB4)
        {
			Name = "Maillet et Ciseau";
		}

        [Constructable]
        public MalletAndChisel(int uses)
            : base(uses, 0x0FB4)
        {
            Weight = 1.0;
			Name = "Maillet et Ciseau";
		}

        public MalletAndChisel(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem => DefMasonry.CraftSystem;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}