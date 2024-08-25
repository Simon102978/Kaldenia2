using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class Ecraseur : Item
	{
		[CommandProperty(AccessLevel.GameMaster)]
		public int Charges { get; set; }

		[Constructable]
		public Ecraseur() : base(4787)
		{
			Name = "Écraseur";
			Weight = 1.0;
			Charges = Utility.RandomMinMax(50, 100);
		}

		public Ecraseur(Serial serial)
			: base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack))
			{
				if (Charges > 0)
				{
					from.SendMessage("Que voulez-vous écraser?");
					from.BeginTarget(1, false, TargetFlags.None, new TargetCallback(OnTarget));
				}
				else
				{
					from.SendMessage("L'écraseur n'a plus de charges.");
				}
			}
			else
			{
				from.SendMessage("L'objet doit être dans votre sac pour être utilisé.");
			}
		}

		private void OnTarget(Mobile from, object o)
		{
			if (Charges <= 0)
			{
				from.SendMessage("L'écraseur n'a plus de charges.");
				return;
			}

			if (o is BaseShell)
			{
				BaseShell shell = (BaseShell)o;

				int amount = Utility.RandomMinMax(1, 3);
				PoudreCoquillages poudre = new PoudreCoquillages(amount);

				if (from.AddToBackpack(poudre))
				{
					from.SendMessage($"Vous avez écrasé le coquillage et obtenu {amount} poudre(s) de coquillages.");
					shell.Consume(1);

					Charges--;
					from.SendMessage($"Il reste {Charges} charges à l'écraseur.");

					if (Charges <= 0)
					{
						from.SendMessage("L'écraseur s'est brisé après cette utilisation.");
						this.Delete();
					}
				}
				else
				{
					poudre.Delete();
					from.SendMessage("Votre sac est trop plein pour contenir la poudre de coquillages.");
				}
			}
			else
			{
				from.SendMessage("Vous ne pouvez écraser cela.");
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Charges: {Charges}");
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)1); // version
			writer.Write((int)Charges);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if (version >= 1)
			{
				Charges = reader.ReadInt();
			}
			else
			{
				Charges = Utility.RandomMinMax(25, 50);
			}
		}
	}
}
