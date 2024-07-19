using Server.Items;
using Server.Targeting;
using Server;

public class OutilDeTanneur : Item
{
	[Constructable]
	public OutilDeTanneur() : base(0x106A)
	{
		Name = "Outil de Tanneur";
		Weight = 1.0;
	}

	public OutilDeTanneur(Serial serial) : base(serial)
	{
	}

	public override void OnDoubleClick(Mobile from)
	{
		from.SendMessage("Sélectionnez le cuir spécial à transformer.");
		from.Target = new InternalTarget(this);
	}

	private class InternalTarget : Target
	{
		private OutilDeTanneur m_Tool;

		public InternalTarget(OutilDeTanneur tool) : base(1, false, TargetFlags.None)
		{
			m_Tool = tool;
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			if (targeted is Item item)
			{
				if (item is BaseLeather)
				{
					BaseLeather leather = (BaseLeather)item;
					int amount = leather.Amount;
					int newAmount = amount / 2;

					if (newAmount > 0)
					{
						leather.Delete();
						from.AddToBackpack(new Leather(newAmount));
						from.SendMessage("Vous avez transformé {0} unités de cuir spécial en {1} unités de cuir régulier.", amount, newAmount);
					}
					else
					{
						from.SendMessage("Il n'y a pas assez de cuir spécial pour le transformer.");
					}
				}
				else
				{
					from.SendMessage("Ceci n'est pas un cuir spécial.");
				}
			}
			else
			{
				from.SendMessage("Ce n'est pas un objet valide.");
			}
		}
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
