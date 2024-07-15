using Server.Engines.Harvest;

namespace Server.Items
{

	public class Shovel : BaseAxe, IHarvestTool
	{

        [Constructable]
        public Shovel()
            : this(50)
        {
        }

        [Constructable]
        public Shovel(int uses)
            : base(0xF39)
        {
            Weight = 5.0;
			Name = "Pelle";
			Layer = Layer.TwoHanded;
        }

		public override void OnDoubleClick(Mobile from)
		{
			if (HarvestSystem == null || Deleted)
				return;

			if (IsChildOf(from.Backpack) || Parent == from)
			{

				bool shovel = this is Shovel;

				if (shovel && Parent != from)
				{
					from.SendMessage("Vous devez avoir l'outil en main pour l'utiliser."); // That must be in your pack for you to use it.
					return;
				}
			}


			Point3D loc = GetWorldLocation();

			if (!from.InLOS(loc) || !from.InRange(loc, 2))
			{
				from.LocalOverheadMessage(Network.MessageType.Regular, 0x3E9, 1019045); // I can't reach that
				return;
			}
			else if (!IsAccessibleTo(from))
			{
				PublicOverheadMessage(Network.MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access 
				return;
			}

			if (!(HarvestSystem is Mining || HarvestSystem is CustomMining))
				from.SendLocalizedMessage(1010018); // What do you want to use this item on?

			HarvestSystem.BeginHarvesting(from, this);
		}

		public Shovel(Serial serial)
            : base(serial)
        {
        }

		public override HarvestSystem HarvestSystem { get { return CustomMining.GetSystem(this); } }


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