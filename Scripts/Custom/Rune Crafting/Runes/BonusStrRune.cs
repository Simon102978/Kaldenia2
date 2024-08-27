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
	public class BonusStrRune : BaseRune
	{

		[Constructable]
		public BonusStrRune() : base()
		{
			Weight = 0.2;  // ?
			Name = "Force";
			Hue = 2101;
		}

		public override bool CanEnchant(Item item, Mobile from)
		{
			if (item is BaseWeapon)
			{
				return true;
			}
			else if (item is BaseArmor)
			{
				return true;
			}
			else if(item is BaseShield)
			{
				return true;
			}
			else if (item is BaseJewel)
			{
				return true;
			}

			from.SendMessage("Vous pouvez enchanter les armes, les armures, les boucliers et les bijoux avec cette rune.");

			return base.CanEnchant(item, from);
		}

		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(5) + 3;

			if (item is BaseWeapon Weapon)
			{				
				Weapon.Attributes.BonusStr += augmentper;								
			}

			else if (item is BaseArmor Armor)
			{			
				Armor.Attributes.BonusStr += augmentper;				
			}

			else if (item is BaseShield Shield)
			{			
						Shield.Attributes.BonusStr += augmentper;					
			}

			else if (item is BaseJewel Jewel)
			{
						Jewel.Attributes.BonusStr += augmentper;								
			}

			base.Enchant(item, from);
		}


		public BonusStrRune( Serial serial ) : base( serial )
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