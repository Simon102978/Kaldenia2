using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Server.Items
{

	public class WallControlerSwitch : Item
	{

		private ArrayList m_doors;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Lock { get; set; }


		[CommandProperty(AccessLevel.GameMaster)]
		public string Emote { get; set; }

		[Constructable]
		public WallControlerSwitch() : base(0x1095)
		{
			LootType = LootType.Blessed;
			Movable = true;
			Name = "Levier";
			m_doors = new ArrayList();
		}

		public WallControlerSwitch(Serial serial) : base(serial)
		{
		}


		public override void OnDoubleClick(Mobile m)
		{
			if (AccessLevel.Player < m.AccessLevel)
			{
				InvalidateProperties();
				m.SendMessage("Please target a wall controler.");
				m.Target = new AddDoor(m_doors);
				InvalidateProperties();
			}
			else if (Lock)
			{
				m.SendMessage("Le levier semble cadenassé");

			}
			else if (CanOpen(m))
			{
				switchit();

			}
		}

		public bool CanOpen(Mobile m)
		{
			if (m.IsStaff())
			{
				m.SendMessage("Du a vos pouvoirs divins, vous ouvrez la porte.");
				return true;
			}
			else if (!m.InLOS(this))
			{
				m.SendMessage("Vous devez avoir la vision du levier pour pouvoir l'activer.");
				return false;
			}
			else if (!m.InRange(this.Location,2))
			{
				m.SendMessage("Vous devez être à moin de deux cases pour activer un levier.");
				return false;
			}
			return true;
		}

		public void switchit()
		{

			if (Emote != null)
			{
				PublicOverheadMessage(MessageType.Regular, 0, false, $"*{Emote}*");
			}

			  WallControlerStone oc;
                foreach (Item i in m_doors)
                {
                    oc = i as WallControlerStone;

                    oc.ActiveSwitch();

                }
        }

		public class AddDoor : Target
		{
            private ArrayList door;
            public AddDoor(ArrayList m_doors) : base(15, false, TargetFlags.None)
			{
                door = m_doors;
			}

            public void switchit()
            {
                WallControlerStone oc;
                foreach (Item i in door)
                {
                    oc = i as WallControlerStone;

                    oc.ActiveSwitch();
                }
            }

			protected override void OnTarget( Mobile from, object targ)
           {

                    if (targ is WallControlerSwitch)
                    {
                        switchit();
                        return;
                    }
                    if (!(targ is WallControlerStone))
                    {
                        from.SendMessage("C'est pas un controleur de Mur.");
                        return;
                    }

                    WallControlerStone d = targ as WallControlerStone;
                    Item targ1 = targ as Item;

                    if (!door.Contains(targ1))
                    {
                        door.Add(targ1);
                        from.SendMessage("Controleur rajouté !");
                    }
                    else
                    {
                        door.Remove(targ1);
                        from.SendMessage("Controleur retiré!");
                    }

			}
		}


		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write(Emote);

            writer.WriteItemList(m_doors);


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();


			switch (version)
			{
				case 1:
					Emote = reader.ReadString();
					goto case 0;
				case 0:
					m_doors = reader.ReadItemList();
					break;
				default:
					break;
			}





			

		}

	}


}