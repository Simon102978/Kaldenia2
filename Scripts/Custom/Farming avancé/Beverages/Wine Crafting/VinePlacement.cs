using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server.Regions;

namespace Server.Misc
{
	public abstract class VinePlacement
	{
		public const int m_VinePrice = 50;

		public static bool ValidatePlacement(Point3D loc, Mobile m_From, object m_Obj)
		{
			Map map = m_From.Map;
			if (map == null)
				return false;

			if (m_From.AccessLevel == AccessLevel.Player)
			{
				return ValidatePlayerPlacement(loc, m_From, map, m_Obj);
			}
			else
			{
				return ValidateAdminPlacement(loc, m_From, map, m_Obj);
			}
		}

		public static bool ValidateAdminPlacement(Point3D loc, Mobile m_From, Map m_Map, object o)
		{
			return true; // Les administrateurs peuvent placer partout
		}

		public static bool ValidatePlayerPlacement(Point3D loc, Mobile m_From, Map m_Map, object o)
		{
			Region region = Region.Find(loc, m_Map);

			if (region is GardenRegion)
			{
				return true; // Autoriser le placement dans une GardenRegion
			}

			m_From.SendMessage("Les vignes doivent être placées dans un jardin.");
			return false;
		}

		public static bool PayForVine(Mobile m_From)
		{
			if (Banker.Withdraw(m_From, m_VinePrice))
			{
				m_From.SendLocalizedMessage(1060398, m_VinePrice.ToString());
				return true;
			}
			else if (m_From.Backpack.ConsumeTotal(typeof(Gold), m_VinePrice))
			{
				m_From.SendMessage(m_VinePrice.ToString() + " pièces d'or ont été retirées de votre sac.");
				return true;
			}
			m_From.SendMessage("Vous n'avez pas assez d'or pour acheter une vigne.");
			return false;
		}

		public static bool RefundForVine(Mobile m_From)
		{
			Container c = m_From.Backpack;
			Gold t = new Gold(m_VinePrice);
			if (c.TryDropItem(m_From, t, true))
			{
				m_From.SendMessage("Vous avez été remboursé de " + m_VinePrice.ToString() + " pièces d'or pour la vigne supprimée.");
				return true;
			}
			else
			{
				t.Delete();
				m_From.SendMessage("Pour une raison quelconque, le remboursement n'a pas fonctionné ! Veuillez contacter un GM.");
				return false;
			}
		}
	}
}
