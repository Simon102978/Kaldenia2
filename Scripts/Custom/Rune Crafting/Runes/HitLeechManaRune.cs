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
	public class HitLeechManaRune : BaseRune
	{
		[Constructable]
		public HitLeechManaRune() : base(  )
		{
			Weight = 0.2;  // ?
			Name = "Vol de Mana";
			Hue = 2075;
		}

		public override bool CanEnchant(Item item, Mobile from)
		{
			if (!(item is BaseWeapon))
			{				
				from.SendMessage("Vous pouvez enchanter que les armes avec cette rune.");
				return false;
			}	

			return base.CanEnchant(item, from);
		}


		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(10) + 5;

			if (item is BaseWeapon Weapon)
			{
				Weapon.WeaponAttributes.HitLeechMana += augmentper;
			}

			base.Enchant(item, from);
		}

		public override bool DisplayLootType { get { return false; } }  // ha ha!

		public HitLeechManaRune(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}