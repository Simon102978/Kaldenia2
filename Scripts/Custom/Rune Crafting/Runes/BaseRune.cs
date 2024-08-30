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
	public abstract class BaseRune : Item
	{


		public virtual int SkillRequis => 70;


		[Constructable]
		public BaseRune() : base( 0x1F14 )
		{
			Weight = 0.2;  // ?
		//	Name = "Hit Harm Rune";
			Hue = 294;
		}

		public virtual bool CanEnchant(Item item, Mobile from)
		{
			if (item is BaseWeapon)
			{
				BaseWeapon weapons = (BaseWeapon)item;
				return weapons.Enchantement < weapons.MaxEnchantements;
			}
			else if (item is BaseArmor)
			{
				BaseArmor armor = (BaseArmor)item;
				return armor.Enchantement < armor.MaxEnchantements;
			}
			else if (item is Spellbook)
			{
				Spellbook spellbook = (Spellbook)item;
				return spellbook.Enchantement < spellbook.MaxEnchantements;
			}
			else if (item is BaseJewel)
			{
				BaseJewel jewel = (BaseJewel)item;
				return jewel.Enchantement < jewel.MaxEnchantements;
			}

			return false;
		}


		public virtual void Enchant(Item item, Mobile from)
		{
			if (CanEnchant(item, from))
			{
				if (item is BaseWeapon weapon)
					weapon.Enchantement++;
				else if (item is BaseArmor armor)
					armor.Enchantement++;
				else if (item is Spellbook spellbook)
					spellbook.Enchantement++;
				else if (item is BaseJewel jewel)
					jewel.Enchantement++;

				from.PlaySound(0x1F5);
				from.SendMessage("Vous avez enchanté l'objet avec succès.");
				this.Delete();
			}
			else
			{
				from.SendMessage("Cet objet ne peut pas recevoir plus d'enchantements.");
			}
		}


		public virtual bool CheckSuccess(Mobile from)
		{
			return true;
			/*		if (!from.CheckSkill(SkillName.Inscribe, SkillRequis, SkillRequis + 30))
					{
						from.SendMessage("La rune explose en mille morceau.");
						from.PlaySound(65);
						from.PlaySound(0x1F8);
						Delete();
						return false;
					}
					else
					{
						return true;
					}*/
		}





		public override void OnDoubleClick(Mobile from)
		{
			PlayerMobile pm = from as PlayerMobile;

			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage("Sélectionnez l'objet à enchanter.");
				from.Target = new InternalTarget(this);
			}
		}

		private class InternalTarget : Target
		{
			private BaseRune m_Rune;

			public InternalTarget(BaseRune rune) : base(1, false, TargetFlags.None)
			{
				m_Rune = rune;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item item)
				{
					if (!m_Rune.CanEnchant(item, from))
					{
						from.SendMessage("Cet objet ne peut pas être enchanté avec cette rune.");
						return;
					}
					else if (!from.InRange(item.GetWorldLocation(), 1))
					{
						from.SendMessage("Vous êtes trop loin de l'objet.");
						return;
					}
					else if ((item.Parent != null) && (item.Parent is Mobile))
					{
						from.SendMessage("Vous ne pouvez pas enchanter cet objet à cet endroit.");
						return;
					}
					else
					{
						m_Rune.Enchant(item, from);
					}
				}
				else
				{
					from.SendMessage("Vous devez cibler un objet.");
				}
			}
		}


		public override bool DisplayLootType{ get{ return false; } }  // ha ha!

		public BaseRune( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version

			writer.Write(Enchantement);

			
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if (version >= 1)
			{
				Enchantement = reader.ReadInt();
			}
		}
	}
}