using Server.ContextMenus;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Items
{
	public class TillerMan : Item
	{
		public virtual bool Babbles => false;
		public BaseBoat Boat { get; private set; }
		private DateTime _NextBabble;

		public bool IsRented { get; private set; }
		public Mobile Renter { get; private set; }
		public DateTime RentalEnd { get; private set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan RentalDuration { get; set; } = TimeSpan.FromHours(1);

		[CommandProperty(AccessLevel.GameMaster)]
		public int RentalCost { get; set; } = 1500;

		public TillerMan(BaseBoat boat)
			: base(0x3E4E)
		{
			Boat = boat;
			Movable = false;
		}

		public TillerMan(Serial serial)
			: base(serial)
		{
		}

		public virtual void SetFacing(Direction dir)
		{
			switch (dir)
			{
				case Direction.South: ItemID = 0x3E4B; break;
				case Direction.North: ItemID = 0x3E4E; break;
				case Direction.West: ItemID = 0x3E50; break;
				case Direction.East: ItemID = 0x3E55; break;
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (Boat.IsRowBoat)
				return;

			list.Add(Boat.Status);
			list.Add(1116580 + (int)Boat.DamageTaken);

			if (IsRented)
			{
				TimeSpan remaining = RentalEnd - DateTime.UtcNow;
				list.Add($"Loué par: {Renter.Name}"); // Loué par: ~1_NAME~

				list.Add($"Temps Restant: {remaining.Hours:D2}:{remaining.Minutes:D2}"); // Temps restant: ~1_VAL~
			}
			else
			{
				list.Add("Bateau à Louer"); // Bateau à louer
			}
		}

		public virtual void Say(int number)
		{
			PublicOverheadMessage(MessageType.Regular, 0x3B2, number);
		}

		public virtual void Say(int number, string args)
		{
			PublicOverheadMessage(MessageType.Regular, 0x3B2, number, args);
		}

		public override void AddNameProperty(ObjectPropertyList list)
		{
			if (Boat != null && Boat.ShipName != null)
				list.Add(1042884, Boat.ShipName); // the tiller man of the ~1_SHIP_NAME~
			else
				base.AddNameProperty(list);
		}

		public Mobile Pilot => Boat?.Pilot;

		public override void OnDoubleClickDead(Mobile from)
		{
			OnDoubleClick(from);
		}
		public override void OnDoubleClick(Mobile from)
		{
			from.RevealingAction();

			if (Boat == null || Boat != BaseBoat.FindBoatAt(from, from.Map))
			{
				from.SendLocalizedMessage(1116724); // You cannot pilot a ship unless you are aboard it!
				Boat?.RemovePilot(from); // Assurez-vous que le pilote est retiré même s'il n'est pas sur le bateau
				return;
			}
			else if (Pilot != null && Pilot != from && (Pilot == Boat.Owner || (IsRented && Pilot == Renter)))
			{
				from.SendLocalizedMessage(502221); // Someone else is already using this item.
			}

			if (from.Flying)
			{
				from.SendLocalizedMessage(1116615); // You cannot pilot a ship while flying!
				Boat.RemovePilot(from);
				return;
			}

			if (Boat.Stuck)
			{
				from.SendMessage("Le bateau est pris, vous ne pouvez pas le déplacer.");
				Boat.RemovePilot(from);
				return;
			}

			if (Pilot == from)
			{
				Boat.RemovePilot(from);
				from.SendLocalizedMessage(502333); // You stop piloting the ship.
			}
			else if (Pilot != null && Pilot != from)
			{
				from.SendLocalizedMessage(502221); // Someone else is already using this item.
			}
			else if (Boat.Scuttled)
			{
				from.SendLocalizedMessage(1116725); // This ship is too damaged to sail!
			}
			else if (!Boat.IsOwner(from) && (!IsRented || Renter != from))
			{
				from.SendMessage("Vous n'êtes pas autorisé à piloter ce bateau.");
			}
			else if (Pilot != null)
			{
				if (from != Pilot) // High authorized player takes control of the ship
				{
					Boat.RemovePilot(Pilot); // Remove the current pilot first
					Boat.LockPilot(from);
				}
				else
				{
					Boat.RemovePilot(from);
				}
			}
			else
			{
				Boat.LockPilot(from);
			}
		}

		public void ForceRemovePilot(Mobile from)
		{
			if (Boat != null)
			{
				Boat.RemovePilot(from);
			}

			if (from.Frozen)
			{
				from.Frozen = false;
			}
		}


		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			if (dropped is MapItem && Boat != null && Boat.CanCommand(from) && Boat.Contains(from))
			{
				Boat.AssociateMap((MapItem)dropped);
			}

			return false;
		}

		public override void OnAfterDelete()
		{
			Boat?.Delete();
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);

			if (Boat.IsRowBoat)
				return;

			if (Boat != null)
			{
				if (Boat.Contains(from))
				{
					if (Boat.IsOwner(from))
						list.Add(new RenameShipEntry(this, from));

					list.Add(new EmergencyRepairEntry(this, from));
					list.Add(new ShipRepairEntry(this, from));

					if (!IsRented)
						list.Add(new RentBoatEntry(this, from));
					else if (from == Renter)
						list.Add(new EndRentalEntry(this, from));
				}
				else if (Boat.IsOwner(from))
				{
					list.Add(new DryDockEntry(Boat, from));
				}
			}
	//		if (!from.Alive)
	//		{
	//			list.Add(new ResurrectEntry(this, from));
	//		}
		}

		public void StartRental(Mobile from)
		{
			if (IsRented)
				return;

			IsRented = true;
			Renter = from;
			RentalEnd = DateTime.UtcNow + RentalDuration;

			Timer.DelayCall(RentalDuration, EndRental);

			from.SendMessage($"Vous avez loué ce bateau pour {RentalDuration.TotalHours} heures.");
		}

		public void EndRental()
		{
			if (!IsRented)
				return;

			IsRented = false;
			if (Renter != null && Renter.NetState != null)
				Renter.SendMessage("Votre location de bateau a expiré.");
			Renter = null;
			RentalEnd = DateTime.MinValue;

			// Replacer le bateau à un endroit spécifique ou le supprimer si nécessaire
			// Boat.MoveToWorld(new Point3D(/* coordonnées du port */), /* carte */);
		}

		public void Babble()
		{
			if (Babbles)
			{
				PublicOverheadMessage(MessageType.Regular, 0x3B2, 1114137, RandomBabbleArgs());
				_NextBabble = DateTime.UtcNow + TimeSpan.FromMinutes(Utility.RandomMinMax(3, 10));
			}
		}

		private string RandomBabbleArgs()
		{
			return string.Format("#{0}\t#{1}\t#{2}", Utility.Random(1114138, 13).ToString(),
													 Utility.Random(1114151, 2).ToString(),
													 Utility.RandomMinMax(1114153, 1114221).ToString());
		}

		public override void OnLocationChange(Point3D oldLocation)
		{
			if (_NextBabble < DateTime.UtcNow && 0.1 > Utility.RandomDouble())
			{
				Babble();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(1); // version

			writer.Write(Boat);
			writer.Write(IsRented);
			writer.Write(Renter);
			writer.Write(RentalEnd);
			writer.Write(RentalDuration);
			writer.Write(RentalCost);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					Boat = reader.ReadItem() as BaseBoat;
					IsRented = reader.ReadBool();
					Renter = reader.ReadMobile();
					RentalEnd = reader.ReadDateTime();
					RentalDuration = reader.ReadTimeSpan();
					RentalCost = reader.ReadInt();

					if (IsRented && DateTime.UtcNow < RentalEnd)
					{
						Timer.DelayCall(RentalEnd - DateTime.UtcNow, EndRental);
					}
					else
					{
						IsRented = false;
						Renter = null;
						RentalEnd = DateTime.MinValue;
					}
					break;
				case 0:
					Boat = reader.ReadItem() as BaseBoat;
					break;
			}

			if (Boat == null)
				Delete();
		}
	/*	private class ResurrectEntry : ContextMenuEntry
		{
			private TillerMan m_TillerMan;
			private Mobile m_From;

			public ResurrectEntry(TillerMan tillerMan, Mobile from) : base(6195, 5) // 6107 est l'index du cliloc pour "Résurrection"
			{
				m_TillerMan = tillerMan;
				m_From = from;
			}

			public override void OnClick()
			{
				if (!m_From.Alive)
				{
					if (Banker.Withdraw(m_From, 500))
					{
						m_From.PlaySound(0x214);
						m_From.FixedEffect(0x376A, 10, 16);

						m_From.Resurrect();
						m_From.SendMessage("Vous avez été ressuscité pour 500 pièces d'or.");
					}
					else
					{
						m_From.SendMessage("Vous n'avez pas assez d'or dans votre banque pour être ressuscité.");
					}
				}
			}
		}*/
		private class EmergencyRepairEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public EmergencyRepairEntry(TillerMan tillerman, Mobile from)
				: base(1116589, 5)
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				if (m_TillerMan?.Boat == null)
					return;

				BaseBoat g = m_TillerMan.Boat;

				if (!g.Scuttled)
					m_From.SendLocalizedMessage(1116595);
				else if (g.IsUnderEmergencyRepairs())
				{
					TimeSpan left = g.GetEndEmergencyRepairs();
					m_From.SendLocalizedMessage(1116592, left != TimeSpan.Zero ? left.TotalMinutes.ToString() : "0");
				}
				else if (!g.TryEmergencyRepair(m_From))
					m_From.SendLocalizedMessage(1116591, $"{BaseBoat.EmergencyRepairClothCost}\t{BaseBoat.EmergencyRepairWoodCost}");
			}
		}

		private class ShipRepairEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public ShipRepairEntry(TillerMan tillerman, Mobile from)
				: base(1116590, 5)
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				if (m_TillerMan?.Boat == null)
					return;

				if (!BaseGalleon.IsNearLandOrDocks(m_TillerMan.Boat))
					m_From.SendLocalizedMessage(1116594);
				else if (m_TillerMan.Boat.DamageTaken == DamageLevel.Pristine)
					m_From.SendLocalizedMessage(1116596);
				else
					m_TillerMan.Boat.TryRepairs(m_From);
			}
		}

		private class RenameShipEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public RenameShipEntry(TillerMan tillerman, Mobile from)
				: base(1111680, 3)
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				if (m_TillerMan?.Boat != null)
					m_TillerMan.Boat.BeginRename(m_From);
			}
		}

		private class RentBoatEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public RentBoatEntry(TillerMan tillerman, Mobile from)
				: base(1062637, 10) // "Louer le bateau"
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				if (m_From.BankBox == null)
				{
					m_From.SendMessage("Vous n'avez pas de compte en banque.");
					return;
				}

				int goldInBank = m_From.BankBox.GetAmount(typeof(Gold));
				if (goldInBank >= m_TillerMan.RentalCost)
				{
					m_From.BankBox.ConsumeTotal(typeof(Gold), m_TillerMan.RentalCost);
					m_TillerMan.StartRental(m_From);
					m_From.SendMessage($"Vous avez loué ce bateau pour {m_TillerMan.RentalDuration.TotalHours} heures. {m_TillerMan.RentalCost} pièces d'or ont été prélevées de votre compte en banque.");
				}
				else
				{
					m_From.SendMessage($"Vous n'avez pas assez d'or dans votre compte en banque pour louer ce bateau. (Coût: {m_TillerMan.RentalCost} gold)");
				}
			}
		}

		private class EndRentalEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public EndRentalEntry(TillerMan tillerman, Mobile from)
				: base(1062638, 10) // "Terminer la location"
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				m_TillerMan.EndRental();
				m_From.SendMessage("Vous avez terminé la location du bateau.");
			}
		}
	}
}