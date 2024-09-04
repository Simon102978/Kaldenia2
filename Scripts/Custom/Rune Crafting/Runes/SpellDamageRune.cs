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
	public class SpellDamageRune : BaseRune
	{
		[Constructable]
		public SpellDamageRune() : base(  )
		{
			Weight = 0.2;  // ?
			Name = "Puissance des sorts";
			Hue = 1266;
		}


		public override bool CanEnchant(Item item, Mobile from)
		{
			if (!(item is BaseWeapon) && !(item is Spellbook))
			{
				
				from.SendMessage("Vous pouvez enchanter que les armes et les livres de sorts avec cette rune.");
				return false;
			}	

			return base.CanEnchant(item, from);
		}



		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(10) + 5;

			if (item is BaseWeapon Weapon)
			{
				Weapon.Attributes.SpellDamage += augmentper;
			}

			if (item is Spellbook spellbook)
			{
				spellbook.Attributes.SpellDamage += augmentper;
			}

			base.Enchant(item, from);
		}

		public override bool DisplayLootType { get { return false; } }  // ha ha!

		public SpellDamageRune(Serial serial) : base(serial)
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