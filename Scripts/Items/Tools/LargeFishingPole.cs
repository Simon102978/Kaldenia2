using System;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.ContextMenus
{
	public class RemoveBaitEntry : ContextMenuEntry
	{
		private Mobile m_From;
		public IFishingPole m_Pole;

		public RemoveBaitEntry(Mobile from, IFishingPole pole) : base(163, 1)
		{
			m_From = from;
			m_Pole = pole;
		}

		public override void OnClick()
		{
			if (!m_From.CheckAlive())
				return;

			if (m_Pole.Bait == Bait.Aucun || m_Pole.Charge <= 0)
				return;

			BaseBait newBait = BaseBait.CreateBait(m_Pole.Bait, m_Pole.Charge);

			if (newBait != null)
				m_From.AddToBackpack(newBait);

			m_Pole.Bait = Bait.Aucun;
			m_Pole.Charge = 0;

			m_From.SendMessage("Vous enlevez l'app�t.");
		}
	}
}

namespace Server.Items
{
    public class LargeFishingPole : Item, IFishingPole
    {
        private Bait m_Bait;
        private int m_Charge;

        [CommandProperty(AccessLevel.GameMaster)]
        public Bait Bait
        {
            get { return m_Bait; }
            set { m_Bait = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charge
        {
            get { return m_Charge; }
            set { m_Charge = value; }
        }

		[Constructable]
		public LargeFishingPole() : base( 0x0DC0 )
        {
            Name = "canne � p�che";
			Layer = Layer.OneHanded;
		}

        public override void OnAosSingleClick(Mobile from)
        {
            base.OnAosSingleClick(from);

            if (m_Bait != Bait.Aucun && m_Charge > 0)
            {
                LabelTo(from, String.Format("[{0} / {1} charge{2}]", BaseBait.m_Material[(int)m_Bait], m_Charge, m_Charge > 1 ? "s" : ""));
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			Fishing.System.BeginHarvesting( from, this );
        }

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			if (from.Alive && m_Bait != Bait.Aucun && m_Charge > 0)
			{
				list.Add(new RemoveBaitEntry(from, this));
			}
			base.GetContextMenuEntries(from, list);

			BaseHarvestTool.AddContextMenuEntries(from, this, list, Fishing.System);
		}
		public override void AddCraftedProperties(ObjectPropertyList list)
		{
			

			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				list.Add(String.Format("[{0} / {1} charge{2}]", BaseBait.m_Material[(int)m_Bait], m_Charge, m_Charge > 1 ? "s" : ""));
			}
			else
				list.Add(String.Format("[ aucun app�t ]"));


		}
		public LargeFishingPole( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

            writer.Write((int)m_Bait);
            writer.Write(m_Charge);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        m_Bait = (Bait)reader.ReadInt();
                        m_Charge = reader.ReadInt();
                        break;
                    }
                case 1:
                    {
                        reader.ReadItem();
                        goto case 0;
                    }
                case 0:
                    {
                        break;
                    }
            }

            Name = "canne � p�che";
		}
	}
}