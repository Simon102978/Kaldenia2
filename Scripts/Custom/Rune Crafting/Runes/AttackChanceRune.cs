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
	public class AttackChanceRune : BaseRune
	{

		[Constructable]
		public AttackChanceRune() : base()
		{
			Weight = 0.2;  // ?
			Name = "Chance de toucher";
			Hue = 2584;
		}

		public override void Enchant(Item item, Mobile from)
		{

			int augmentper = Utility.Random(10) + 5;

			if (item is BaseWeapon Weapon)
			{				
				Weapon.Attributes.AttackChance += augmentper;								
			}

			base.Enchant(item, from);
		}

		public override bool DisplayLootType{ get{ return false; } }  // ha ha!

		public AttackChanceRune( Serial serial ) : base( serial )
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