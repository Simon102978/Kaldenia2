using System; 
using Server; 
using Server.Network; 
using Server.Targeting; 
using System.Collections; 
using Server.Mobiles; 

namespace Server.Items 
{ 
   	public class Larmedesirene : BaseBeverage 
   	{
		private const int V = 0xA5;

		public override int MaxQuantity{ get{ return 10; } } 
      		public override bool Fillable{ get{ return false; } } 

		public override int ComputeItemID() 
      		{ 
         		if ( !IsEmpty ) 
         		{ 
            			return 0x99B; 
         		} 

         		return 0; 
      		} 

      		[Constructable] 
      		public Larmedesirene() : base() 
      		{  
         	Weight = 1.0; 
            Hue = 1940;
         	Name = "Larmes de Sirène";   
         	Quantity = 10;
         	ItemID = 0x99B;
			Description = "Bière à saveur de café et une larme de Torgah. 6.9%";
			Crafter = null;
			Quality = ItemQuality.Legendary;
		} 

      		public Larmedesirene( Serial serial ) : base( serial ) 
      		{ 
      		} 

      		public override void Serialize( GenericWriter writer ) 
      		{ 
         		base.Serialize( writer ); 

         		writer.Write( (int) 0 ); // version 
      		} 

      		public override void Deserialize( GenericReader reader ) 
      		{ 
         		base.Deserialize( reader ); 

         		int version = reader.ReadInt(); 
      		} 
   	} 
}