using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Custom.Misc;
using Server.Network;
using Server.Gumps;
using Server.ContextMenus;

namespace Server.Mobiles
{
 
	public class Passeur : BaseVendor
	{
		#region Variables

		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();

		protected override List<SBInfo> SBInfos => m_SBInfos;
		public override bool IsActiveVendor => false;
		public override bool IsActiveBuyer => false;
		public override bool IsActiveSeller => false;
		public override bool CanTeach => false;

		public override void InitSBInfo()
		{ }


        private Point3D m_PointDest;
        private Map m_MapDest;

        private int m_Price;

        private string m_DestinationName;

        private bool m_Active;
		
		#endregion

		#region Get


        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D PointDest
        {
            get { return m_PointDest; }
            set
            {
                m_PointDest = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map MapDest
        {
            get { return m_MapDest; }
            set
            {
                m_MapDest = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Price
        {
            get { return m_Price; }
            set
            {
                m_Price = value;
               
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string DestinationName
        {
            get { return m_DestinationName; }
            set
            {
                m_DestinationName = value;
               
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get { return m_Active; }
            set
            {
                m_Active = value;
               
            }
        }


		#endregion

		#region Methods



        #endregion

        #region Actions


        #endregion

        #region Constructor
        [Constructable]
        public Passeur() : base("Passeur")
        {
// 1015182
//

        }

        public Passeur(Serial serial) : base( serial )
        {

        }

        public override void OnDoubleClick(Mobile from)
        {
			if (from is CustomPlayerMobile cm)
			{
                if(MapDest == null || PointDest == new Point3D() || !Active )
				{
                   Say("Je ne peux faire la traverser en se moment.");
				}               
                else
                {
                	from.SendGump(new PasseurConfirmationGump(cm, this));
                }
			
			}
          
        }

        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
         
           
           if (Active)
           {
              list.Add(new PasseurTeleportEntry(from, this));
           }
            

            base.AddCustomContextEntries(from, list);
        }
        #endregion

        #region Serialize/Deserialize
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_PointDest);
            writer.Write(m_MapDest);
            writer.Write(m_Price);
            writer.Write(m_DestinationName);
            writer.Write(m_Active);


        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)    
            {
                case 0:
                {
                    m_PointDest = reader.ReadPoint3D();
                    m_MapDest = reader.ReadMap();
                    m_Price = reader.ReadInt();
                    m_DestinationName = reader.ReadString();
                    m_Active = reader.ReadBool();
                    break;
                }
            }

        }
        #endregion
    }    

    
}

namespace Server.ContextMenus
{
    public class PasseurTeleportEntry : ContextMenuEntry
    {
        private readonly Passeur m_Vendor;
        private Mobile m_From;

        public PasseurTeleportEntry(Mobile from, Passeur vendor)
            : base(1015182, 8)
        {
            m_Vendor = vendor;
            Enabled = vendor.Active;
            m_From = from;
        }

        public override void OnClick()
        {
            m_Vendor.OnDoubleClick(m_From);
        }
    }
}       
        
namespace Server.Gumps
{

	public class PasseurConfirmationGump : BaseProjectMGump
	{
		CustomPlayerMobile m_from;
		Passeur m_Passeur;



		public PasseurConfirmationGump(CustomPlayerMobile from,  Passeur passeur)
		   : base("Passeur",375, 225)
		{
			m_from = from;
			m_Passeur = passeur;
            int x = XBase;
            int y = YBase;

    //        AddBackground(36+movex, 25+movey, 369, 170, 9270);
     //       AddBackground(54+movex, 43+movey, 334, 28, 3000);


                
	//		AddLabel(77+movex, 47+movey, 0, "Cela vous coutera " + passeur.Price + " pièces d'or.");
	        AddSection(x, y, 405, 195, m_Passeur.DestinationName, $"Pour traverser vers {m_Passeur.DestinationName}, cela vous coutera {m_Passeur.Price} pièces d'or.");

            AddBackground(x, y + 197, 405, 75, 9270);
			
			AddButton(130+x,215+y, 1147, 1148, 1, GumpButtonType.Reply, 0);
            AddButton(210+x,215+y, 1144, 1145, 0, GumpButtonType.Reply, 0);
		}



		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 0)
			{
			
			}
			else
			{			
				if(m_Passeur.MapDest == null || m_Passeur.PointDest == new Point3D() || !m_Passeur.Active )
				{
                    m_from.SendMessage("Ce passeur n'est présentement pas en activité.");
				}
                else if (!BaseVendor.ConsumeGold(m_from.Backpack,m_Passeur.Price))
				{
					m_from.SendMessage("Vous n'avez pas l'or nécessaire pour effectuer ce passage.");
				}
                else
                {              
                    m_from.SendMessage("Vous effectuez la traverser.");
                    m_from.MoveToWorld(m_Passeur.PointDest, m_Passeur.MapDest);
                }		
			}
		}
	}
}