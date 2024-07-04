using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class SetAncienChest : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienChest() : this(0) { }

		[Constructable]
		public SetAncienChest(int hue) : base(0xA481)
		{
			Name = "Plastron Akseh";
			Weight = 4.0;
			SetSkillBonuses.SetValues(0, SkillName.Parry, 25);
			SetHue = 1305;
		}

		public SetAncienChest(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 5;
		public override int BaseColdResistance => 5;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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

	public class SetAncienPants : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienPants() : this(0) { }

		[Constructable]
		public SetAncienPants(int hue) : base(0xA480)
		{
			Name = "Pantalon Akseh";
			Weight = 5.0;
			SetSkillBonuses.SetValues(0, SkillName.Swords, 25);
			SetHue = 1305;
			SetAttributes.BonusStr = 10;
		}

		public SetAncienPants(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 4;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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

	public class SetAncienArms : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienArms() : this(0) { }

		[Constructable]
		public SetAncienArms(int hue) : base(0xA482)
		{
			Name = "Brassards Akseh";
			Weight = 2.0;
			SetAttributes.BonusDex = 10;
			SetHue = 1305;
		}

		public SetAncienArms(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 4;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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

	public class SetAncienGloves : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienGloves() : this(0) { }

		[Constructable]
		public SetAncienGloves(int hue) : base(0xA483)
		{
			Name = "Gants Akseh";
			Weight = 2.0;
			SetSkillBonuses.SetValues(0, SkillName.Fencing, 25);
			SetHue = 1305;
		}

		public SetAncienGloves(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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

	public class SetAncienHelm : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienHelm() : this(0) { }

		[Constructable]
		public SetAncienHelm(int hue) : base(0xA485) 
		{
			Name = "Heaume Akseh";
			Weight = 5.0;
			SetAttributes.BonusInt = 10;
			SetAttributes.BonusHits = 50;

			SetHue = 1305;
		}

		public SetAncienHelm(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 8;
		public override int BaseFireResistance => 5;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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

	public class SetAncienGorget : BaseArmor
	{
		public override SetItem SetID { get { return SetItem.GuerrierAncien; } }
		public override int Pieces { get { return 6; } }

		[Constructable]
		public SetAncienGorget() : this(0) { }

		[Constructable]
		public SetAncienGorget(int hue) : base(0xA484)
		{
			Name = "Gorget Akseh";
			Weight = 3.0;
			SetAttributes.BonusHits = 50;
			SetHue = 1305;
		}

		public SetAncienGorget(Serial serial) : base(serial) { }

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 4;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 150;
		public override int StrReq => 65;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

		public override void OnAdded(object parent)
		{
			base.OnAdded(parent);
			CheckSetBonus(parent as Mobile);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);
			CheckSetBonus(parent as Mobile);
		}

		private void CheckSetBonus(Mobile wearer)
		{
			if (wearer == null)
				return;

			int pieces = 0;

			foreach (Item item in wearer.Items)
			{
				if (item is BaseArmor armor && armor.SetID == this.SetID)
				{
					pieces++;
				}
			}

			if (pieces >= 3)
			{
				StatMod mod = wearer.GetStatMod("SetAncienStrBonus");
				if (mod == null || mod.Offset != 20)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienStrBonus", 20, TimeSpan.Zero));
				}
			}
			else
			{
				wearer.RemoveStatMod("SetAncienStrBonus");
			}

			if (pieces == 6)
			{
				StatMod hpMod = wearer.GetStatMod("SetAncienHpBonus");
				if (hpMod == null || hpMod.Offset != 50)
				{
					wearer.AddStatMod(new StatMod(StatType.Str, "SetAncienHpBonus", 50, TimeSpan.Zero));
				}

				// Effet visuel
				wearer.FixedParticles(0x376A, 9, 3200, 5052, SetHue, 0, EffectLayer.Waist);
			}
			else
			{
				wearer.RemoveStatMod("SetAncienHpBonus");
			}
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
