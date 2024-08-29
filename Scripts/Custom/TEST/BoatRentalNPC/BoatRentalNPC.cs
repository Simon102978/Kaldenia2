using Server.Mobiles;
using Server;
using System.Collections.Generic;
using Server.Items;
using Server.Multis;
using System;
using System.Linq;
using Server.Targeting;

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
			from.SendMessage("Sélectionnez l'emplacement où vous souhaitez placer votre bateau de location.");
			from.Target = new InternalTarget(this, from);
		}
		else
		{
			from.SendMessage("Vous n'avez pas assez d'or pour louer un bateau.");
		}
	}

	private class InternalTarget : Target
	{
		private BoatRentalNPC m_NPC;
		private Mobile m_From;

		public InternalTarget(BoatRentalNPC npc, Mobile from) : base(-1, true, TargetFlags.None)
		{
			m_NPC = npc;
			m_From = from;
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			if (targeted is IPoint3D point)
			{
				Point3D loc = new Point3D(point);
				Map map = from.Map;

				if (map == null)
				{
					from.SendMessage("Vous ne pouvez pas placer un bateau ici.");
					RefundGold(from);
					return;
				}

				// Vérifiez si la zone ciblée est de l'eau
			
				// Ajustez la hauteur du bateau
				loc.Z = map.GetAverageZ(loc.X, loc.Y);

				Galleon boat = new Galleon(Direction.North);
				boat.Owner = from;

				// Vérifiez si le bateau peut être placé à cet endroit
				if (boat.CanFit(loc, map, boat.ItemID))
				{
					boat.MoveToWorld(loc, map);

					BoatRental rental = new BoatRental(from, boat, m_NPC.DefaultRentalDuration);
					m_NPC.ActiveRentals[from] = rental;
					rental.RentalTimer = Timer.DelayCall(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30), () => m_NPC.CheckRentalTime(from));

					from.SendMessage($"Vous avez loué un bateau pour {m_NPC.DefaultRentalDuration.TotalHours} heures.");
				}
				else
				{
					from.SendMessage("Le bateau ne peut pas être placé à cet endroit. Choisissez un autre emplacement.");
					RefundGold(from);
				}
			}
			else
			{
				from.SendMessage("Vous devez cibler une zone d'eau pour placer le bateau.");
				RefundGold(from);
			}
		}


		private void RefundGold(Mobile from)
		{
			from.AddToBackpack(new Gold(1000));
			from.SendMessage("Votre or a été remboursé.");
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
			if (rental.Boat != null)
			{
				rental.Boat.Delete();
			}
			rental.RentalTimer.Stop();
			ActiveRentals.Remove(from);
			from.SendMessage("Votre location de bateau a expiré. Le bateau a été retourné.");
		}
		InvalidateProperties();
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
			kvp.Value.Serialize(writer);
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
			BoatRental rental = new BoatRental(reader);
			if (key != null)
			{
				ActiveRentals[key] = rental;
				rental.RentalTimer = Timer.DelayCall(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30), () => CheckRentalTime(key));
			}
		}
	}
}

