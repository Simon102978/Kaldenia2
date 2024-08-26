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
	public class HitLeechLifeRune : BaseRune
	{
		[Constructable]
		public HitLeechLifeRune() : base( 0x1F14 )
		{
			Weight = 0.2;  // ?
			Name = "Vol de Vie";
			Hue = 2075;
		}

		public override bool CanEnchant(Item item, Mobile from)
		{
			if (item is BaseWeapon)
			{
				return true;
			}

			from.SendMessage("Vous pouvez enchanter que les armes avec cette rune.");

			return base.CanEnchant(item, from);
		}

		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(10) + 5;

			if (item is BaseWeapon Weapon)
			{
				Weapon.WeaponAttributes.HitLeechHits += augmentper;
			}

			base.Enchant(item, from);
		}

		public override bool DisplayLootType { get { return false; } }  // ha ha!

		public HitLeechLifeRune(Serial serial) : base(serial)
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