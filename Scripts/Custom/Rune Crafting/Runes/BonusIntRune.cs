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
	public class BonusIntRune : BaseRune
	{

		[Constructable]
		public BonusIntRune() : base()
		{
			Weight = 0.2;  // ?
			Name = "Intelligence";
			Hue = 2101;
		}



		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(5) + 3;

			if (item is BaseWeapon Weapon)
			{				
				Weapon.Attributes.BonusInt += augmentper;								
			}

			else if (item is BaseArmor Armor)
			{			
				Armor.Attributes.BonusInt += augmentper;				
			}

			else if (item is BaseJewel Jewel)
			{
						Jewel.Attributes.BonusInt += augmentper;								
			}

			base.Enchant(item, from);
		}


		public BonusIntRune( Serial serial ) : base( serial )
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