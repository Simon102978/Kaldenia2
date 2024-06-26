using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Custom.Packaging.Packages;

namespace Server.Items
{
	public class SkillStone75 : Item
	{
		[Constructable]
		public SkillStone75() : base(0xED4) // You can use any item ID you want for the stone
		{
			Movable = false;
			Hue = 1160; // You can change the hue if you want
			Name = "Test Stone 75";
		}

		public SkillStone75(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (from is CustomPlayerMobile player)
			{
				foreach (Skill skill in player.Skills)
				{
					skill.Base = 75.0; // Set all skills to 75
				}

				player.SendMessage("Tous vos skills sont à 75.0 + 50000 en banque");
				player.BankBox.AddItem(new Gold(50000));
				player.BankBox.AddItem(new Materiaux(5000));
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
