using System;
using Server;
using Server.Targeting;
using Server.Multis;
using Server.Gumps;

namespace Server.Items
{
	public class CopyToolbox : Item
	{
		private bool m_Used;

		[Constructable]
		public CopyToolbox() : base(0x1EB8)
		{
			Name = "Boîte à outils de Charpente";
			m_Used = false;
		}

		public CopyToolbox(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Used)
			{
				from.SendMessage("Cette boîte à outils a déjà été utilisée.");
				return;
			}

			BaseHouse house = BaseHouse.FindHouseAt(from);

			if (house == null || !house.IsOwner(from))
			{
				from.SendMessage("Vous devez être dans une maison dont vous êtes propriétaire pour utiliser cet outil.");
				return;
			}

			from.SendMessage("Sélectionnez l'élément que vous souhaitez copier.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private CopyToolbox m_Toolbox;

			public InternalTarget(CopyToolbox toolbox) : base(-1, true, TargetFlags.None)
			{
				m_Toolbox = toolbox;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Toolbox.Deleted || m_Toolbox.m_Used)
					return;

				BaseHouse house = BaseHouse.FindHouseAt(from);

				if (house == null || !house.IsOwner(from))
				{
					from.SendMessage("Vous devez être dans une maison dont vous êtes propriétaire pour utiliser cet outil.");
					return;
				}

				if (targeted is StaticTarget)
				{
					StaticTarget t = (StaticTarget)targeted;
					int itemID = t.ItemID;

					from.SendMessage("Vous avez copié l'ID de l'élément : " + itemID);
					m_Toolbox.m_Used = true;
					m_Toolbox.ItemID = itemID;
					m_Toolbox.Name = "Élément copié (ID: " + itemID + ")";
				}
				
				else
				{
					from.SendMessage("Vous ne pouvez copier que des éléments statiques");
				}
			}
		}

		
		
		

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
			writer.Write(m_Used);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Used = reader.ReadBool();
		}
	}
}
