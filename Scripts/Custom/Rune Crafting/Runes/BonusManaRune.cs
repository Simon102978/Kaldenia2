using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Spells;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	public class BonusManaRune : BaseRune
	{

		[Constructable]
		public BonusManaRune() : base()
		{
			Weight = 0.2;  // ?
			Name = "Mana";
			Hue = 2101;
		}



		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(10) + 5;

			if (item is BaseWeapon Weapon)
			{				
				Weapon.Attributes.BonusMana += augmentper;								
			}

			else if (item is BaseArmor Armor)
			{			
				Armor.Attributes.BonusMana += augmentper;				
			}

			else if (item is BaseShield Shield)
			{			
						Shield.Attributes.BonusMana += augmentper;					
			}

			else if (item is BaseJewel Jewel)
			{
						Jewel.Attributes.BonusMana += augmentper;								
			}

			base.Enchant(item, from);
		}


		public BonusManaRune( Serial serial ) : base( serial )
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