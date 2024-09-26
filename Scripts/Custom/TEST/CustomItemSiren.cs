using System;
using Server;
using Server.Prompts;

namespace Server.Items
{
	public class ItemChangerSiren : Item
	{
		private bool m_Used;

		[Constructable]
		public ItemChangerSiren() : base(0x1EBA)
		{
			Name = "à renommer";
			Weight = 10.0;
			m_Used = false;
		}

		public ItemChangerSiren(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Used)
			{
				return;
			}

			if (from.Skills[SkillName.Tinkering].Value < 90.0)
			{
				from.SendMessage("Vous avez besoin d'au moins 90 en Tinkering pour utiliser cet objet.");
				return;
			}

			from.SendMessage("Entrez l'ID de l'item :");
			from.Prompt = new ItemIDPrompt(this);
		}

		public void SetUsed()
		{
			m_Used = true;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)1); // version
			writer.Write(m_Used);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if (version >= 1)
			{
				m_Used = reader.ReadBool();
			}
		}
	}

	public class ItemIDPrompt : Prompt
	{
		private ItemChangerSiren m_Item;

		public ItemIDPrompt(ItemChangerSiren item)
		{
			m_Item = item;
		}

		public override void OnResponse(Mobile from, string text)
		{
			if (int.TryParse(text, out int itemID))
			{
				m_Item.ItemID = itemID;
				m_Item.SetUsed();
				from.SendMessage("L'ID de l'item a été changé en : " + itemID);
			}
			else
			{
				from.SendMessage("Veuillez entrer un nombre valide.");
			}
		}
	}
}
