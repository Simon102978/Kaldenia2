using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class SkillStone : Item
	{
		[Constructable]
		public SkillStone() : base(0xED4) // You can use any item ID you want for the stone
		{
			Movable = false;
			Hue = 1170; // You can change the hue if you want
			Name = "Test Stone 100";

		}

		public SkillStone(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (from is CustomPlayerMobile player)
			{
				foreach (Skill skill in player.Skills)
				{
					skill.Base = 100.0; // Set all skills to 100
				}

				player.SendMessage("Tous vos skills sont à 100 + 50000 en banque");
				player.BankBox.AddItem(new Gold(50000));
				player.Niveau = 30;
				player.FENormalTotal = 500;
			}
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
