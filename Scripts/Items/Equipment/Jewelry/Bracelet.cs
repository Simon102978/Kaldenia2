using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class BaseBracelet : BaseJewel
    {
        public BaseBracelet(int itemID)
            : base(itemID, Layer.Bracelet)
        {
        }

        public BaseBracelet(Serial serial)
            : base(serial)
        {
        }

        public override int BaseGemTypeNumber => 1044221;// star sapphire bracelet
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(2); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 1)
            {
                if (Weight == .1)
                {
                    Weight = -1;
                }
            }
        }
    }

    public class GoldBracelet : BaseBracelet
    {
        [Constructable]
        public GoldBracelet()
            : base(0x1086)
        {
			Name = "Bracelet doré";
		}

        public GoldBracelet(Serial serial)
            : base(serial)
        {
        }

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

    public class SilverBracelet : BaseBracelet, IRepairable
    {
        public CraftSystem RepairSystem => DefTinkering.CraftSystem;

        [Constructable]
        public SilverBracelet()
            : base(0x1F06)
        {
			Name = "Bracelet argenté";
        }

        public SilverBracelet(Serial serial)
            : base(serial)
        {
        }

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
	public class BraceletMontre : BaseBracelet, IRepairable
	{
		public CraftSystem RepairSystem => DefTinkering.CraftSystem;

		[Constructable]
		public BraceletMontre()
			: base(0x1F06)
		{
			Name = "Montre solaire";
		}

		public BraceletMontre(Serial serial)
			: base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsEquippedBy(from))
			{
				from.SendMessage("Vous devez porter la montre solaire pour l'utiliser.");
				return;
			}

			int hours, minutes;
			Clock.GetTime(from.Map, from.X, from.Y, out hours, out minutes);

			if (hours >= 22 || hours < 6)
			{
				from.SendMessage("Il fait trop sombre pour lire l'heure sur votre montre solaire.");
				return;
			}

			string timeString = $"{hours:D2}:{minutes:D2}";
			from.SendMessage($"Votre montre solaire indique qu'il est {timeString}.");
		}

		private bool IsEquippedBy(Mobile m)
		{
			return m.FindItemOnLayer(Layer.Bracelet) == this;
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
