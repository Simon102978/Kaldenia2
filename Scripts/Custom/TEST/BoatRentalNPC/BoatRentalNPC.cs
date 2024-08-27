using Server.Mobiles;
using Server;
using System.Collections.Generic;
using Server.Items;
using Server.Multis;
using System;
using System.Linq;

public class BoatRentalNPC : BaseVendor
{
	private static List<SBInfo> m_SBInfos = new List<SBInfo>();
	protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

	public Dictionary<Mobile, BoatRental> ActiveRentals { get; set; } = new Dictionary<Mobile, BoatRental>();

	[CommandProperty(AccessLevel.GameMaster)]
	public TimeSpan DefaultRentalDuration { get; set; } = TimeSpan.FromHours(3);

	[CommandProperty(AccessLevel.GameMaster)]
	public int ActiveRentalCount => ActiveRentals.Count;

	[CommandProperty(AccessLevel.GameMaster)]
	public string ActiveRentalInfo
	{
		get
		{
			if (ActiveRentals.Count == 0)
				return "Aucune location active";

			return string.Join("\n", ActiveRentals.Select(r =>
				$"{r.Key.Name}: {r.Value.GetRemainingTime().Hours}h {r.Value.GetRemainingTime().Minutes}m restantes"));
		}
	}
	[Constructable]
	public BoatRentalNPC() : base("l'agent de location de bateaux")
	{
	}

	public BoatRentalNPC(Serial serial) : base(serial)
	{
	}

	public override void InitSBInfo()
	{
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (from.InRange(this.Location, 3))
		{
			if (ActiveRentals.ContainsKey(from))
			{
				from.SendMessage("Vous avez déjà un bateau en location.");
				return;
			}
			from.SendGump(new BoatRentalGump(from, this));
		}
		else
		{
			from.SendLocalizedMessage(500446); // C'est trop loin.
		}
	}

	public void StartBoatRental(Mobile from)
	{
		if (from.Backpack.ConsumeTotal(typeof(Gold), 1000))
		{
			from.SendMessage("Démarrage de la location de bateau.");
			FinishBoatRental(from);
		}
		else
		{
			from.SendMessage("Vous n'avez pas assez d'or pour louer un bateau.");
		}
	}

	public void FinishBoatRental(Mobile from)
	{
		from.SendMessage("Début du processus de location.");

		if (from.Backpack.ConsumeTotal(typeof(Gold), 1000))
		{
			from.SendMessage("1000 pièces d'or ont été prélevées.");

			MediumBoatDeed deed = new MediumBoatDeed();
			from.SendMessage("Deed de bateau créé.");

			if (from.AddToBackpack(deed))
			{
				from.SendMessage("Deed ajouté à votre sac.");

				BoatRental rental = new BoatRental(from, deed, DefaultRentalDuration);
				ActiveRentals[from] = rental;

				rental.RentalTimer = Timer.DelayCall(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30), () => CheckRentalTime(from));

				from.SendMessage($"Location enregistrée. Vous avez loué un bateau pour {DefaultRentalDuration.TotalHours} heures.");
				from.SendMessage("Utilisez le deed pour placer votre bateau sur l'eau.");

				InvalidateProperties(); // Force la mise à jour des propriétés
			}
			else
			{
				deed.Delete();
				from.AddToBackpack(new Gold(1000));
				from.SendMessage("Impossible d'ajouter le deed à votre sac. Location annulée et remboursée.");
			}
		}
		else
		{
			from.SendMessage("Vous n'avez pas assez d'or pour louer un bateau.");
		}
	}


	private void CheckRentalTime(Mobile from)
	{
		if (ActiveRentals.TryGetValue(from, out BoatRental rental))
		{
			TimeSpan remaining = rental.EndTime - DateTime.Now;
			if (remaining <= TimeSpan.Zero)
			{
				EndRental(from);
			}
			else
			{
				from.SendMessage($"Il vous reste {remaining.Hours} heures et {remaining.Minutes} minutes de location pour votre bateau.");
			}
		}
	}

	private void EndRental(Mobile from)
	{
		if (ActiveRentals.TryGetValue(from, out BoatRental rental))
		{
			// Supprimer le deed du sac du joueur s'il y est toujours
			from.Backpack?.RemoveItem(rental.Deed);

			// Si le bateau a été placé, le supprimer
			if (rental.Deed.Boat != null)
			{
				rental.Deed.Boat.Delete();
			}

			// Supprimer le deed
			rental.Deed.Delete();

			rental.RentalTimer.Stop();
			ActiveRentals.Remove(from);
			from.SendMessage("Votre location de bateau a expiré. Le bateau et son deed ont été retournés.");
		}
			InvalidateProperties(); // Force la mise à jour des propriétés
	}

	public override void GetProperties(ObjectPropertyList list)
	{
		base.GetProperties(list);

		if (ActiveRentals.Count > 0)
		{
			list.Add($"Locations actives : {ActiveRentals.Count}");
			foreach (var rental in ActiveRentals)
			{
				TimeSpan remaining = rental.Value.EndTime - DateTime.UtcNow;
				if (remaining > TimeSpan.Zero)
				{
					list.Add($"{rental.Key.Name}: {remaining.Hours}h {remaining.Minutes}m restantes");
				}
				else
				{
					list.Add($"{rental.Key.Name}: Location expirée");
				}
			}
		}
		else
		{
			list.Add("Aucune location active");
		}

		// Propriété pour les GameMasters
		if (AccessLevel.GameMaster <= AccessLevel)
		{
			list.Add($"[GM] Durée de location par défaut: {DefaultRentalDuration.TotalHours} heures");
		}
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write((int)0); // version

		writer.Write(ActiveRentals.Count);
		foreach (var kvp in ActiveRentals)
		{
			writer.Write(kvp.Key);
			writer.Write(kvp.Value.Renter);
			writer.Write(kvp.Value.Deed);
			writer.Write(kvp.Value.EndTime);
		}
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();

		int count = reader.ReadInt();
		for (int i = 0; i < count; i++)
		{
			Mobile key = reader.ReadMobile();
			Mobile renter = reader.ReadMobile();
			MediumBoatDeed deed = reader.ReadItem() as MediumBoatDeed;
			DateTime endTime = reader.ReadDateTime();

			if (key != null && renter != null && deed != null)
			{
				BoatRental rental = new BoatRental(renter, deed, endTime - DateTime.Now);
				ActiveRentals[key] = rental;
				rental.RentalTimer = Timer.DelayCall(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30), () => CheckRentalTime(key));
			}
		}
	}
}

