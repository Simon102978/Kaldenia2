using Server.ContextMenus;
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
		public TimeSpan RentalDuration { get; set; } = TimeSpan.FromHours(3);

		[CommandProperty(AccessLevel.GameMaster)]
		public int RentalCost { get; set; } = 1000;

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

				if (remaining != null)
				{
					list.Add($"Temps restant: {remaining.Hours:D2}:{remaining.Minutes:D2}");
				}
				else
				{
					list.Add("Temps restant: Non disponible");
				}

				// Vérifiez que Renter n'est pas null et que Name n'est pas vide
				if (Renter != null && !string.IsNullOrEmpty(Renter.Name))
				{
					list.Add($"Loué par: {Renter.Name}");
				}
				else
				{
					list.Add("Loué par: Non spécifié");
				}
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

		public override void OnDoubleClickDead(Mobile m)
		{
			OnDoubleClick(m);
		}

		public override void OnDoubleClick(Mobile from)

		{
			from.RevealingAction();

			BaseBoat boat = BaseBoat.FindBoatAt(from, from.Map);
			Item mount = from.FindItemOnLayer(Layer.Mount);

			if (boat == null || Boat == null || Boat != boat)
			{
				from.SendLocalizedMessage(1116724); // You cannot pilot a ship unless you are aRegularBoard it!
			}
			else if (Pilot != null && Pilot != from && (Pilot == Boat.Owner || (IsRented && Pilot == Renter)))
			{
				from.SendLocalizedMessage(502221); // Someone else is already using this item.
			}
			else if (from.Flying)
			{
				from.SendLocalizedMessage(1116615); // You cannot pilot a ship while flying!
			}
			else if (!Boat.IsOwner(from) && !IsRented && Renter != from)
			{
				from.SendMessage("Vous n'êtes pas autorisé à piloter ce bateau.");
			}
			else if (boat.Stuck)
			{
				from.SendMessage("Le bateau est pris, vous ne pouvez pas le déplacer.");
			}
			else if (from.Mounted && !(mount is BoatMountItem))
			{
				from.SendLocalizedMessage(1010097); // You cannot use this while mounted or flying.
			}
			else if (Pilot == null && Boat.Scuttled)
			{
				from.SendLocalizedMessage(1116725); // This ship is too damaged to sail!
			}
			else if (Pilot != null)
			{
				if (from != Pilot) // High authorized player takes control of the ship
				{
					boat.RemovePilot(from);
					boat.LockPilot(from);
				}
				else
				{
					boat.RemovePilot(from);
				}
			}
			else
			{
				boat.LockPilot(from);
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

			if (Boat != null && Boat.Contains(from))
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
				: base(1011530, 10) // "Louer le bateau"
			{
				m_TillerMan = tillerman;
				m_From = from;
			}

			public override void OnClick()
			{
				if (m_From.Backpack.ConsumeTotal(typeof(Gold), m_TillerMan.RentalCost))
				{
					m_TillerMan.StartRental(m_From);
				}
				else
				{
					m_From.SendMessage($"Vous n'avez pas assez d'or pour louer ce bateau. (Coût: {m_TillerMan.RentalCost} gold)");
				}
			}
		}

		private class EndRentalEntry : ContextMenuEntry
		{
			private readonly TillerMan m_TillerMan;
			private readonly Mobile m_From;

			public EndRentalEntry(TillerMan tillerman, Mobile from)
				: base(503207, 10) // "Terminer la location"
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
