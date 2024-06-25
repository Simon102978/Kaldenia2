using System; 
using Server; 
using Server.Items; 
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Multis;

namespace Server.Items 
{ 
   public class GardenSign : BaseAddon 
   { 
      private BaseGarden m_Garden;
      [Constructable] 
      public GardenSign( BaseGarden garden) 
      { 
         m_Garden = garden;
         Name = "Jardin"; 
         
         
         this.ItemID = 3025;
         this.Visible = true; 
      }

      public override void OnDoubleClick( Mobile from ) 
      { 
            if (m_Garden.Owner==from || from.IsStaff())
            {
               from.SendGump(new GardenGump((CustomPlayerMobile)from,m_Garden));
            } 
            else if(m_Garden.Owner == null && !m_Garden.Public)
            {
               from.SendGump(new GardenRentGump((CustomPlayerMobile)from,m_Garden));
            }
            else 
            { 
             from.SendMessage( "Ce jardin ne vous appartient pas." ); 
            } 
      } 


      public GardenSign( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 0 ); // version 
         writer.Write(m_Garden);

      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
         
         switch (version)
         {
            case 0:
            {
               m_Garden = reader.ReadItem<BaseGarden>();
               break;
            }
         }

      } 

   } 
}
